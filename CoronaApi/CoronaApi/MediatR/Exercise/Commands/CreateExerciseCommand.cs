using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Db;
using CoronaApi.Db.Types;
using CoronaApi.Dtos;
using CoronaApi.Mapping;
using CoronaApi.MediatR.Core.CommandHandlers;
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

        public class CreateExerciseCommandHandler : BaseCreateCommandHandler<CreateExerciseCommand, ExerciseDto, DbExercise>
        {
            public CreateExerciseCommandHandler(IMapper mapper, ApplicationDbContext dbContext) : base(mapper, dbContext)
            {
            }
        }
    }
}