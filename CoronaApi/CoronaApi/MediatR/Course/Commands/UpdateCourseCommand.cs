using System;
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

namespace CoronaApi.MediatR.Course.Commands
{
    public class UpdateCourseCommand : IRequest<CourseDto>, IMapFrom<DbCourse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SubjectId { get; set; }
        public string TeacherId { get; set; }


        public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
        {
            public UpdateCourseCommandValidator(UserManager<ApplicationUser> userManager,
                                                ApplicationDbContext applicationDbContext,
                                                IHttpContextAccessor contextAccessor)
            {
                this.RuleFor(c => c.Name)
                    .NotEmpty();
                this.RuleFor(c => c.TeacherId)
                    .NotEmpty();
                this.RuleFor(c => c.SubjectId)
                    .NotEmpty();
                this.RuleFor(command => command.TeacherId)
                    .MustAsync(async (s, token) =>
                    {
                        var teacher = await applicationDbContext.ApplicationUsers.SingleOrDefaultAsync(user => user.Id == s);
                        return teacher != null;
                    });
                this.RuleFor(command => command.Id)
                    .NotEmpty();
                this.RuleFor(command => command.Id)
                    .MustAsync(async (courseId, cancellationToken) =>
                    {
                        var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                        var courseIsInSchool = applicationDbContext.Courses.Any(course => course.Id == courseId && course.Subject.SchoolId == user.SchoolId);
                        return user.UserType != UserType.Student && courseIsInSchool;
                    })
                    .WithMessage("Unauthorized");
            }
        }

        public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public UpdateCourseCommandHandler(IMapper mapper, ApplicationDbContext dbContext)
            {
                this._mapper = mapper;
                this._dbContext = dbContext;
            }

            public async Task<CourseDto> Handle(UpdateCourseCommand request,
                                                CancellationToken cancellationToken)
            {
                var existing =
                    await this._dbContext.Courses.SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                    ?? throw new NotFoundException(nameof(DbCourse), request.Id);
                this._mapper.Map(request, existing);

                this._dbContext.Update(existing);

                return this._mapper.Map<CourseDto>(existing);
            }
        }
    }
}

