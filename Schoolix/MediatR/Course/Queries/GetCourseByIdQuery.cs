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

namespace Schoolix.MediatR.Course.Queries
{
    public class GetCourseByIdQuery : IRequest<CourseWithRelationsDto>
    {
        public Guid Id { get; set; }

        public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseWithRelationsDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserManager<ApplicationUser> _userManager;

            public GetCourseByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager,
                                             IHttpContextAccessor contextAccessor)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
                this._userManager = userManager;
                this._httpContextAccessor = contextAccessor;
            }

            public async Task<CourseWithRelationsDto> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
            {
                var user = await this._userManager.GetUserAsync(this._httpContextAccessor.HttpContext.User);

                var result = await this._dbContext.Courses.Where(e => e.Subject.SchoolId == user.SchoolId)
                                       .ProjectTo<CourseWithRelationsDto>(this._mapper.ConfigurationProvider)
                                       .SingleOrDefaultAsync(e => e.Id == query.Id, cancellationToken);

                if (result == null)
                {
                    throw new NotFoundException(nameof(CourseWithRelationsDto), query.Id);
                }

                return result;
            }
        }
    }
}