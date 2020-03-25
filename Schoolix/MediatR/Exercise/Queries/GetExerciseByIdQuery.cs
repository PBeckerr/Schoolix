using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Schoolix.Core;
using Schoolix.Db;
using Schoolix.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Schoolix.MediatR.Exercise.Queries
{
    public class GetExerciseByIdQuery : IRequest<ExerciseDto>
    {
        public Guid Id { get; set; }

        public class GetAllExerciseQueryHandler : IRequestHandler<GetExerciseByIdQuery, ExerciseDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetAllExerciseQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                this._dbContext = dbContext;
                this._mapper = mapper;
            }

            public async Task<ExerciseDto> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
            {
                return await this._dbContext.Exercises
                                 .ProjectTo<ExerciseDto>(this._mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync(dto => dto.Id == request.Id, cancellationToken)
                       ?? throw new NotFoundException(nameof(GetAllExerciseQuery), request.Id);
            }
        }
    }
}