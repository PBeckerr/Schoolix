using System;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Db;
using CoronaApi.Db.Types;
using CoronaApi.Dtos;
using CoronaApi.Mapping;
using CoronaApi.MediatR.Core.CommandHandlers;
using CoronaApi.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CoronaApi.MediatR.SchoolYear.Commands
{
    public class CreateSchoolYearCommand: IRequest<SchoolYearDto>, IMapFrom<DbSchoolYear>
    {
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public Guid SchoolId { get; set; }

        public class CreateSchoolYearCommandValidator : AbstractValidator<CreateSchoolYearCommand>
        {
            public CreateSchoolYearCommandValidator(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
            {
                RuleFor(c => c.Begin)
                    .NotEmpty()
                    .LessThan(c => c.End);
                RuleFor(c => c.End)
                    .NotEmpty();
                RuleFor(c => c.SchoolId)
                    .NotEmpty();
                RuleFor(command => command.SchoolId)
                    .MustAsync(async (schoolId, cancellationToken) =>
                    {
                        var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                        return user.SchoolId == schoolId;
                    });
            }
        }

        public class
            CreateSchoolYearCommandHandler : BaseCreateCommandHandler<CreateSchoolYearCommand, SchoolYearDto,
                DbSchoolYear>
        {
            public CreateSchoolYearCommandHandler(IMapper mapper, ApplicationDbContext dbContext) : base(mapper, dbContext)
            {
            }
        }
    }
}
