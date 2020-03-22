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
using CoronaApi.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoronaApi.MediatR.Submission.Commands
{
    public class UpdateSubmissionCommand : IRequest<SubmissionDto>, IMapFrom<DbSubmission>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid ExerciseId { get; set; }

        public IFormFileCollection Files { get; set; }
        public List<Guid> DeletedFiles { get; set; }

        public class UpdateSubmissionCommandValidator : AbstractValidator<UpdateSubmissionCommand>
        {
            public UpdateSubmissionCommandValidator(UserManager<ApplicationUser> userManager,
                                                    ApplicationDbContext applicationDbContext,
                                                    IHttpContextAccessor contextAccessor)
            {
                this.RuleFor(command => command.Date)
                    .NotEmpty();
                this.RuleFor(command => command.Id)
                    .NotEmpty();
                this.RuleFor(command => command.ExerciseId)
                    .NotEmpty();

                this.RuleFor(command => command.ExerciseId)
                    .MustAsync(async (exerciseId, cancellationToken) =>
                    {
                        var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                        var dbExercise = applicationDbContext.Exercises.Include(exercise => exercise.Course.Subject)
                                                             .Single(exercise => exercise.Id == exerciseId);
                        return user.SchoolId == dbExercise.Course.Subject.SchoolId;
                    })
                    .WithMessage("Unauthorized");
            }
        }

        public class UpdateSubmissionCommandHandler : IRequestHandler<UpdateSubmissionCommand, SubmissionDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IMapper _mapper;

            public UpdateSubmissionCommandHandler(IMapper mapper, ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor)
            {
                this._mapper = mapper;
                this._dbContext = dbContext;
                this._httpContextAccessor = contextAccessor;
            }

            public async Task<SubmissionDto> Handle(UpdateSubmissionCommand request, CancellationToken cancellationToken)
            {
                var existing = await this._dbContext.Submissions.SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                               ?? throw new NotFoundException(nameof(DbSubmission), request.Id);
                this._mapper.Map(request, existing);

                await this.SaveFiles(request, existing);
                this._dbContext.Update(existing);

                return this._mapper.Map<SubmissionDto>(existing);
            }

            public async Task SaveFiles(UpdateSubmissionCommand request, DbSubmission dbModel)
            {
                var course =
                    await this._dbContext.Courses.SingleOrDefaultAsync(dbCourse => dbCourse.Exercises.Any(exercise => exercise.Id == request.ExerciseId))
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

                this._dbContext.ExerciseFiles.RemoveRange(this._dbContext.ExerciseFiles.Where(file => request.DeletedFiles.Contains(file.Id)));
            }
        }
    }
}