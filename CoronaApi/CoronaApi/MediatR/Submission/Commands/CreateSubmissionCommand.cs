using System;
using System.IO;
using System.Linq;
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
using Microsoft.EntityFrameworkCore;

namespace CoronaApi.MediatR.Submission.Commands
{
    public class CreateSubmissionCommand : IRequest<SubmissionDto>, IMapFrom<DbSubmission>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid ExerciseId { get; set; }

        public IFormFileCollection Files { get; set; }

        public class CreateSubmissionCommandValidator : AbstractValidator<CreateSubmissionCommand>
        {
            public CreateSubmissionCommandValidator()
            {
                this.RuleFor(command => command.Date)
                    .NotEmpty();
                this.RuleFor(command => command.ExerciseId)
                    .NotEmpty();
                this.RuleFor(command => command.Files)
                    .NotEmpty();
            }
        }

        public class CreateSubmissionCommandHandler : BaseCreateCommandHandler<CreateSubmissionCommand, SubmissionDto, DbSubmission>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateSubmissionCommandHandler(IMapper mapper, ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : base(
                mapper,
                dbContext)
            {
                this._httpContextAccessor = contextAccessor;
            }

            public override async Task CustomCreateLogicAsync(CreateSubmissionCommand request, DbSubmission dbModel)
            {
                var course =
                    await this.DbContext.Courses.SingleOrDefaultAsync(dbCourse => dbCourse.Exercises.Any(exercise => exercise.Id == request.ExerciseId))
                    ?? throw new NotFoundException(nameof(DbCourse), request.ExerciseId);

                //TODO: maybe smarter logic
                foreach (var requestFile in request.Files)
                {
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), Statics.FileBasePath, course.Id.ToString());
                    Directory.CreateDirectory(folder);
                    var fullFilePath = Path.Combine(folder, requestFile.FileName);
                    await using var fileWriter = new FileStream(fullFilePath, FileMode.Create);
                    await requestFile.CopyToAsync(fileWriter);
                    await fileWriter.FlushAsync();

                    var staticFilePath = StaticUrl.Get(this._httpContextAccessor, course.Id.ToString(), requestFile.Name);

                    dbModel.SubmissionFiles.Add(new DbSubmissionFile
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