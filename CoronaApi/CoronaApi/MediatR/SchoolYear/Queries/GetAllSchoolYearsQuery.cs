using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoronaApi.Db;
using CoronaApi.Dtos;
using CoronaApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoronaApi.MediatR.SchoolYear.Queries
{
    public class GetAllSchoolYearsQuery : IRequest<List<SchoolYearDto>>
    {
        public ApplicationUser User { get; set; }
        
        public class GetAllSchoolYearsQueryHandler : IRequestHandler<GetAllSchoolYearsQuery, List<SchoolYearDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            
            public GetAllSchoolYearsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            
            public Task<List<SchoolYearDto>> Handle(GetAllSchoolYearsQuery query, CancellationToken cancellationToken)
            {
                return _dbContext.SchoolYears.Where(e => e.SchoolId == query.User.SchoolId)
                    .ProjectTo<SchoolYearDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            }
        }
    }
}
