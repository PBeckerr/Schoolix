using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Schoolix.Core;
using Schoolix.Db;
using Schoolix.Db.Types;
using Schoolix.Dtos;
using Schoolix.Mapping;
using Schoolix.MediatR.Core.CommandHandlers;
using Schoolix.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Schoolix.MediatR.Submission.Commands
{
    public class CreateSubmissionCommand : IRequest<SubmissionDto>, IMapFrom<DbSubmission>
    {
        public DateTime Date { get; set; }

        public Guid ExerciseId { get; set; }

        public IFormFileCollection Files { get; set; }

        public class CreateSubmissionCommandValidator : AbstractValidator<CreateSubmissionCommand>
        {
            public CreateSubmissionCommandValidator(UserManager<ApplicationUser> userManager,
                                                    ApplicationDbContext applicationDbContext,
                                                    IHttpContextAccessor contextAccessor)
            {
                this.RuleFor(command => command.Date)
                    .NotEmpty();
                this.RuleFor(command => command.ExerciseId)
                    .NotEmpty();
                this.RuleFor(command => command.Files)
                    .NotEmpty();
                this.RuleFor(command => command.ExerciseId)
                    .MustAsync(async (exerciseId, cancellationToken) =>
                    {
                        var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                        var isInCourse = applicationDbContext.Exercises
                                                             .Any(exercise => exercise.Id == exerciseId && exercise.Course.StudentRelations.Any(relation => relation.StudentId == user.Id));
                        return isInCourse;
                    })
                    .WithMessage("Unauthorized");
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