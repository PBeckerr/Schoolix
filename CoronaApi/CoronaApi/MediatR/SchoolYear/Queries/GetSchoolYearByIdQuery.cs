using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoronaApi.Core;
using CoronaApi.Db;
using CoronaApi.Dtos;
using CoronaApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoronaApi.MediatR.SchoolYear.Queries
{
    public class GetSchoolYearByIdQuery : IRequest<SchoolYearDto>
    {
        public ApplicationUser User { get; set; }

        public Guid Id { get; set; }

        public class GetAllSchoolYearsQueryHandler : IRequestHandler<GetSchoolYearByIdQuery, SchoolYearDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetAllSchoolYearsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<SchoolYearDto> Handle(GetSchoolYearByIdQuery query, CancellationToken cancellationToken)
            {
                var result = await _dbContext.SchoolYears.Where(e => e.SchoolId == query.User.SchoolId)
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
