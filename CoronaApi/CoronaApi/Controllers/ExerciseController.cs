using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaApi.Dtos;
using CoronaApi.MediatR.Exercise.Commands;
using CoronaApi.MediatR.Exercise.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoronaApi.Controllers
{
    [Authorize]
    public class ExerciseController : BaseV1ApiController
    {
        [HttpPost]
        [Authorize(Policy = Statics.TeacherOrSchoolClaim)]
        public async Task<ExerciseDto> Post([FromForm] CreateExerciseCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<ExerciseDto>> GetAll([FromQuery] GetAllExerciseQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpGet("my")]
        public async Task<List<ExerciseDto>> MyExercises([FromQuery] GetMyExerciseQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ExerciseDto> GetById(GetExerciseByIdQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Statics.TeacherOrSchoolClaim)]
        public async Task<ExerciseDto> Put([FromRoute] Guid id, [FromForm] UpdateExerciseCommand query)
        {
            query.Id = id;
            return await this.Mediator.Send(query);
        }
    }
}