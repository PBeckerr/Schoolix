using System;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Db;
using CoronaApi.Db.Types;
using CoronaApi.Dtos;
using CoronaApi.Mapping;
using CoronaApi.MediatR.Core.CommandHandlers;
using CoronaApi.MediatR.Course.Commands;
using CoronaApi.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CoronaApi.MediatR.Subject.Commands
{
    public class CreateSubjectCommand : IRequest<SubjectDto>, IMapFrom<DbSubject>
    {
        public string Name { get; set; }

        public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
        {
            public CreateSubjectCommandValidator()
            {
                RuleFor(c => c.Name)
                    .MaximumLength(50)
                    .NotEmpty();
            }
        }

        public class CreateSubjectCommandHandler : BaseCreateCommandHandler<CreateSubjectCommand, SubjectDto, DbSubject>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserManager<ApplicationUser> _userManager;

            public CreateSubjectCommandHandler(ApplicationDbContext dbContext, IMapper mapper,
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor contextAccessor) : base(mapper, dbContext)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
                this._userManager = userManager;
                this._httpContextAccessor = contextAccessor;
            }

            public override async Task CustomCreateLogicAsync(CreateSubjectCommand request, DbSubject dbModel)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                dbModel.SchoolId = user.SchoolId;
            }
        }
    }
}
