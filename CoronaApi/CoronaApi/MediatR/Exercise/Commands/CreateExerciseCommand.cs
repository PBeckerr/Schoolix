using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Db;
using CoronaApi.Db.Types;
using CoronaApi.Dtos;
using CoronaApi.Mapping;
using FluentValidation;
using MediatR;

namespace CoronaApi.MediatR.Exercise.Commands
{
    public class CreateExerciseCommand : IRequest<ExerciseDto>, IMapFrom<DbExercise>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CourseId { get; set; }

        public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
        {
            public CreateExerciseCommandValidator()
            {
                this.RuleFor(command => command.Title)
                    .MaximumLength(255)
                    .NotEmpty();
                this.RuleFor(command => command.ExpirationDate)
                    .NotEmpty();
                this.RuleFor(command => command.CourseId)
                    .NotEmpty();
            }
        }

        public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ExerciseDto>
        {
            private readonly IMapper _mapper;
            private readonly ApplicationDbContext _dbContext;

            public CreateExerciseCommandHandler(IMapper mapper, ApplicationDbContext dbContext)
            {
                this._mapper = mapper;
                this._dbContext = dbContext;
            }
            public async Task<ExerciseDto> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
            {
                var mapped = this._mapper.Map<DbExercise>(request);
                var newEntity = await this._dbContext.Exercises.AddAsync(mapped, cancellationToken);
                await this._dbContext.SaveChangesAsync(cancellationToken);
                return this._mapper.Map<ExerciseDto>(newEntity);
            }
        }
    }
}