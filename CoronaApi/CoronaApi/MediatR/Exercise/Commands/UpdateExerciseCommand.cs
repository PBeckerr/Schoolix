using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Core;
using CoronaApi.Db;
using CoronaApi.Db.Types;
using CoronaApi.Dtos;
using CoronaApi.Mapping;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CoronaApi.MediatR.Exercise.Commands
{
    public class UpdateExerciseCommand : IRequest<ExerciseDto>, IMapFrom<DbExercise>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CourseId { get; set; }

        public IFormFileCollection Files { get; set; }

        public List<Guid> DeletedFiles { get; set; }

        public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
        {
            public UpdateExerciseCommandValidator()
            {
                this.RuleFor(command => command.Id)
                    .NotEmpty();
                this.RuleFor(command => command.Title)
                    .MaximumLength(100)
                    .NotEmpty();
                this.RuleFor(command => command.ExpirationDate)
                    .NotEmpty();
                this.RuleFor(command => command.CourseId)
                    .NotEmpty();
            }
        }

        public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, ExerciseDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IMapper _mapper;

            public UpdateExerciseCommandHandler(IMapper mapper, ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor)
            {
                this._mapper = mapper;
                this._dbContext = dbContext;
                this._httpContextAccessor = contextAccessor;
            }

            public async Task<ExerciseDto> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
            {
                var existing = await this._dbContext.Exercises.SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                               ?? throw new NotFoundException(nameof(DbExercise), request.Id);
                this._mapper.Map(request, existing);

                await this.SaveFiles(request, existing);
                this._dbContext.Update(existing);

                return this._mapper.Map<ExerciseDto>(existing);
            }

            public async Task SaveFiles(UpdateExerciseCommand request, DbExercise dbModel)
            {
                //TODO: maybe smarter logic
                foreach (var requestFile in request.Files)
                {
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), Statics.FileBasePath, request.CourseId.ToString());
                    Directory.CreateDirectory(folder);
                    var fullFilePath = Path.Combine(folder, requestFile.FileName);
                    await using var fileWriter = new FileStream(fullFilePath, FileMode.Create);
                    await requestFile.CopyToAsync(fileWriter);
                    await fileWriter.FlushAsync();

                    var staticFilePath =
                        $"{this._httpContextAccessor.HttpContext.Request.Scheme}://{this._httpContextAccessor.HttpContext.Request.Host}{this._httpContextAccessor.HttpContext.Request.PathBase}{Statics.FileBasePath}/{requestFile.Name}";

                    dbModel.ExerciseFiles.Add(new DbExerciseFile
                    {
                        File = new DbFile
                        {
                            Name = requestFile.FileName,
                            Url = staticFilePath
                        }
                    });
                }

                this._dbContext.ExerciseFiles.RemoveRange(this._dbContext.ExerciseFiles.Where(file => request.DeletedFiles.Contains(file.Id)));
            }
        }
    }
}
