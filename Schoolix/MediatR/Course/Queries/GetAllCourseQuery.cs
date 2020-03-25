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

namespace Schoolix.MediatR.Course.Queries
{
    public class GetAllCourseQuery : IRequest<List<CourseWithRelationsDto>>
    {
        public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCourseQuery, List<CourseWithRelationsDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserManager<ApplicationUser> _userManager;

            public GetAllCoursesQueryHandler(ApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager,
                                                 IHttpContextAccessor contextAccessor)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
                this._userManager = userManager;
                this._httpContextAccessor = contextAccessor;
            }

            public async Task<List<CourseWithRelationsDto>> Handle(GetAllCourseQuery query, CancellationToken cancellationToken)
            {
                var user = await this._userManager.GetUserAsync(this._httpContextAccessor.HttpContext.User);

                return await this._dbContext.Courses.Where(e => e.Teacher.SchoolId == user.SchoolId)
                                 .ProjectTo<CourseWithRelationsDto>(this._mapper.ConfigurationProvider)
                                 .ToListAsync(cancellationToken);
            }
        }
    }
}