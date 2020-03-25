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
using Microsoft.EntityFrameworkCore;

namespace Schoolix.MediatR.Submission.Queries
{
    public class GetAllSubmissionsQuery : IRequest<List<SubmissionDto>>
    {
        public Guid ExerciseId { get; set; }

        public class GetAllExerciseQueryHandler : IRequestHandler<GetAllSubmissionsQuery, List<SubmissionDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetAllExerciseQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
            }

            public Task<List<SubmissionDto>> Handle(GetAllSubmissionsQuery request, CancellationToken cancellationToken)
            {
                return this._dbContext.Submissions
                           .ProjectTo<SubmissionDto>(this._mapper.ConfigurationProvider)
                           .Where(exercise => exercise.ExerciseId == request.ExerciseId)
                           .ToListAsync(cancellationToken);
            }
        }
    }
}