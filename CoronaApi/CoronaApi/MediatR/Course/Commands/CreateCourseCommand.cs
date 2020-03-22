using AutoMapper;
using CoronaApi.Db;
using CoronaApi.Db.Types;
using CoronaApi.Dtos;
using CoronaApi.Mapping;
using CoronaApi.MediatR.Core.CommandHandlers;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoronaApi.MediatR.Course.Commands
{
    public class CreateCourseCommand : IRequest<CourseDto>, IMapFrom<DbCourse>
    {
        public string Name { get; set; }

        public SubjectDto Subject { get; set; }
        public string TeacherId { get; set; }

        public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
        {
            public CreateCourseCommandValidator(ApplicationDbContext applicationDbContext)
            {
                this.RuleFor(c => c.Name)
                    .NotEmpty();
                this.RuleFor(c => c.TeacherId)
                    .NotEmpty();
                this.RuleFor(command => command.TeacherId)
                    .MustAsync(async (s, token) =>
                    {
                        var teacher = await applicationDbContext.ApplicationUsers.SingleOrDefaultAsync(user => user.Id == s);
                        return teacher != null;
                    });
            }
        }

        public class CreateCourseCommandHandler : BaseCreateCommandHandler<CreateCourseCommand, CourseDto, DbCourse>
        {
            public CreateCourseCommandHandler(IMapper mapper, ApplicationDbContext dbContext) : base(mapper, dbContext)
            {
            }
        }
    }
}