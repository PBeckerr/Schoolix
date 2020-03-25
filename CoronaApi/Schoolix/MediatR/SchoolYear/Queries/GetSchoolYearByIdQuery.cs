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

namespace Schoolix.MediatR.SchoolYear.Queries
{
    public class GetSchoolYearByIdQuery : IRequest<SchoolYearDto>
    {
        public Guid Id { get; set; }

        public class GetAllSchoolYearsQueryHandler : IRequestHandler<GetSchoolYearByIdQuery, SchoolYearDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private IHttpContextAccessor _httpContextAccessor;
            private UserManager<ApplicationUser> _userManager;

            public GetAllSchoolYearsQueryHandler(ApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _userManager = userManager;
                _httpContextAccessor = contextAccessor;
            }

            public async Task<SchoolYearDto> Handle(GetSchoolYearByIdQuery query, CancellationToken cancellationToken)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                
                var result = await _dbContext.SchoolYears.Where(e => e.SchoolId == user.SchoolId)
                    .ProjectTo<SchoolYearDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(e => e.Id == query.Id, cancellationToken);

                if (result == null)
                {
                    throw new NotFoundException(nameof(SchoolYearDto), query.Id);
                }

                return result;
            }
        }
    }
}
