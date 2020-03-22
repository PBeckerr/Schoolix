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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoronaApi.MediatR.Course.Queries
{
    public class GetMyCoursesQuery : IRequest<List<CourseDto>>
    {
        public class GetMyCourseQueryHandler : IRequestHandler<GetMyCoursesQuery, List<CourseDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserManager<ApplicationUser> _userManager;

            public GetMyCourseQueryHandler(ApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager,
                                           IHttpContextAccessor contextAccessor)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
                this._userManager = userManager;
                this._httpContextAccessor = contextAccessor;
            }

            public async Task<List<CourseDto>> Handle(GetMyCoursesQuery query, CancellationToken cancellationToken)
            {
                var user = await this._userManager.GetUserAsync(this._httpContextAccessor.HttpContext.User);

                return await this._dbContext.Courses.Where(e => e.StudentRelations.Any(relation => relation.StudentId == user.Id))
                                 .ProjectTo<CourseDto>(this._mapper.ConfigurationProvider)
                                 .ToListAsync(cancellationToken);
            }
        }
    }
}