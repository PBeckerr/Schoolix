using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Schoolix.Core;
using Schoolix.Db;
using Schoolix.Dtos;
using Schoolix.MediatR.Exercise.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Schoolix.MediatR.Submission.Queries
{
    public class GetSubmissionByIdQuery : IRequest<SubmissionDto>
    {
        public Guid Id { get; set; }

        public class GetAllExerciseQueryHandler : IRequestHandler<GetSubmissionByIdQuery, SubmissionDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetAllExerciseQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
            }

            public async Task<SubmissionDto> Handle(GetSubmissionByIdQuery request, CancellationToken cancellationToken)
            {
                return await this._dbContext.Submissions
                                 .ProjectTo<SubmissionDto>(this._mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync(dto => dto.Id == request.Id, cancellationToken)
                       ?? throw new NotFoundException(nameof(GetAllExerciseQuery), request.Id);
            }
        }
    }
}