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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoronaApi.MediatR.Exercise.Queries
{
    public class GetMyExerciseQuery : IRequest<List<ExerciseDto>>
    {
        public Guid CourseId { get; set; }

        public class GetMyExerciseQueryHandler : IRequestHandler<GetMyExerciseQuery, List<ExerciseDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly UserManager<ApplicationUser> _userManager;

            public GetMyExerciseQueryHandler(ApplicationDbContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
                this._contextAccessor = contextAccessor;
                this._userManager = userManager;
            }

            public async Task<List<ExerciseDto>> Handle(GetMyExerciseQuery request, CancellationToken cancellationToken)
            {
                var student = await this._userManager.GetUserAsync(this._contextAccessor.HttpContext.User);

                return await this._dbContext.Exercises
                           .Where(exercise => exercise.Course.StudentRelations.Any(relation => relation.StudentId == student.Id))
                           .ProjectTo<ExerciseDto>(this._mapper.ConfigurationProvider)
                           .ToListAsync(cancellationToken);
            }
        }
    }
}