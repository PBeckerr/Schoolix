using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Schoolix.Db;
using Schoolix.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Schoolix.MediatR.Exercise.Queries
{
    public class GetAllExerciseQuery : IRequest<List<ExerciseDto>>
    {
        public Guid CourseId { get; set; }

        public class GetAllExerciseQueryHandler : IRequestHandler<GetAllExerciseQuery, List<ExerciseDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetAllExerciseQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
            }

            public Task<List<ExerciseDto>> Handle(GetAllExerciseQuery request, CancellationToken cancellationToken)
            {
                return this._dbContext.Exercises
                           .ProjectTo<ExerciseDto>(this._mapper.ConfigurationProvider)
                           .Where(exercise => exercise.CourseId == request.CourseId)
                           .ToListAsync(cancellationToken);
            }
        }
    }
}