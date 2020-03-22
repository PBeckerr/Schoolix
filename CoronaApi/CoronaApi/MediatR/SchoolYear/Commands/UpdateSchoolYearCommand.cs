using System;
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

namespace CoronaApi.MediatR.SchoolYear.Commands
{
    public class UpdateSchoolYearCommand : IRequest<SchoolYearDto>, IMapFrom<DbSchoolYear>
    {
        public Guid Id { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public Guid SchoolId { get; set; }


        public class UpdateSchoolYearCommandValidator : AbstractValidator<UpdateSchoolYearCommand>
        {
            public UpdateSchoolYearCommandValidator(UserManager<ApplicationUser> userManager,
                IHttpContextAccessor contextAccessor)
            {
                RuleFor(c => c.Id)
                    .NotEmpty();
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

        public class UpdateSchoolYearCommandHandler : IRequestHandler<UpdateSchoolYearCommand, SchoolYearDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public UpdateSchoolYearCommandHandler(IMapper mapper, ApplicationDbContext dbContext)
            {
                _mapper = mapper;
                _dbContext = dbContext;
            }

            public async Task<SchoolYearDto> Handle(UpdateSchoolYearCommand request,
                CancellationToken cancellationToken)
            {
                var existing =
                    await this._dbContext.SchoolYears.SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                    ?? throw new NotFoundException(nameof(DbSchoolYear), request.Id);
                _mapper.Map(request, existing);

                _dbContext.Update(existing);

                return _mapper.Map<SchoolYearDto>(existing);
            }
        }
    }
}
