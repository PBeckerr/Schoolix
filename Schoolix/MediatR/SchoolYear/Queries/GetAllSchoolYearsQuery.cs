using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Schoolix.Db;
using Schoolix.Dtos;
using Schoolix.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Schoolix.MediatR.SchoolYear.Queries
{
    public class GetAllSchoolYearsQuery : IRequest<List<SchoolYearDto>>
    {
        public class GetAllSchoolYearsQueryHandler : IRequestHandler<GetAllSchoolYearsQuery, List<SchoolYearDto>>
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
            
            public async Task<List<SchoolYearDto>> Handle(GetAllSchoolYearsQuery query, CancellationToken cancellationToken)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                
                return await _dbContext.SchoolYears.Where(e => e.SchoolId == user.SchoolId)
                    .ProjectTo<SchoolYearDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            }
        }
    }
}
