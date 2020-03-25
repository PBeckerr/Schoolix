using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Schoolix.Core;
using Schoolix.Db;
using Schoolix.Dtos;
using Schoolix.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Schoolix.MediatR.Subject.Queries
{
    public class GetSubjectByIdQuery : IRequest<SubjectDto>
    {
        public Guid Id { get; set; }
        
        public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, SubjectDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserManager<ApplicationUser> _userManager;

            public GetSubjectByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager,
                IHttpContextAccessor contextAccessor)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
                this._userManager = userManager;
                this._httpContextAccessor = contextAccessor;
            }
            
            public async Task<SubjectDto> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await this._userManager.GetUserAsync(this._httpContextAccessor.HttpContext.User);

                var result = await this._dbContext.Subjects.Where(e => e.SchoolId == user.SchoolId)
                    .ProjectTo<SubjectDto>(this._mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                if (result == null)
                {
                    throw new NotFoundException(nameof(SubjectDto), request.Id);
                }

                return result;
            }
        }
    }
}
