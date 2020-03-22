using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaApi.Dtos;
using CoronaApi.MediatR.Course.Commands;
using CoronaApi.MediatR.Course.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoronaApi.Controllers
{
    [Authorize]
    public class CourseController : BaseV1ApiController
    {
        [HttpPost]
        [Authorize(Policy = Statics.TeacherOrSchoolClaim)]
        public async Task<CourseDto> Create(CreateCourseCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<CourseWithRelationsDto>> GetAll([FromQuery] GetAllCourseQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpGet("my")]
        public async Task<List<CourseWithRelationsDto>> MyCourses([FromQuery] GetMyCoursesQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<CourseWithRelationsDto> GetById([FromRoute] GetCourseByIdQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Statics.TeacherOrSchoolClaim)]
        public async Task<CourseDto> Update([FromRoute] Guid id, [FromBody] UpdateCourseCommand command)
        {
            command.Id = id;
            return await this.Mediator.Send(command);
        }
    }
}