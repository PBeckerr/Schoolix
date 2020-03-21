using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Core;
using CoronaApi.Db;
using CoronaApi.Db.Types;
using CoronaApi.Dtos;
using CoronaApi.Mapping;
using CoronaApi.MediatR.Core.CommandHandlers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CoronaApi.MediatR.Exercise.Commands
{
    public class CreateExerciseCommand : IRequest<ExerciseDto>, IMapFrom<DbExercise>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CourseId { get; set; }

        public IFormFileCollection Files { get; set; }

        public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
        {
            public CreateExerciseCommandValidator()
            {
                this.RuleFor(command => command.Title)
                    .MaximumLength(100)
                    .NotEmpty();
                this.RuleFor(command => command.ExpirationDate)
                    .NotEmpty();
                this.RuleFor(command => command.CourseId)
                    .NotEmpty();
                this.RuleFor(command => command.Files)
                    .NotEmpty();
            }
        }

        public class CreateExerciseCommandHandler : BaseCreateCommandHandler<CreateExerciseCommand, ExerciseDto, DbExercise>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateExerciseCommandHandler(IMapper mapper, ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : base(mapper, dbContext)
            {
                this._httpContextAccessor = contextAccessor;
            }

            public override async Task CustomCreateLogicAsync(CreateExerciseCommand request, DbExercise dbModel)
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

                    var staticFilePath = StaticUrl.Get(this._httpContextAccessor, request.CourseId.ToString(), requestFile.Name);

                    dbModel.ExerciseFiles.Add(new DbExerciseFile
                    {
                        File = new DbFile
                        {
                            Name = requestFile.FileName,
                            Url = staticFilePath
                        }
                    });
                }
            }
        }
    }
}