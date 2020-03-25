using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Schoolix.Dtos;
using Schoolix.MediatR.Subject.Commands;
using Schoolix.MediatR.Subject.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Schoolix.Controllers
{
    public class SubjectController : BaseV1ApiController
    {
        [HttpPost]
        [Authorize(Policy = Statics.SchoolClaim)]
        public async Task<SubjectDto> Create(CreateSubjectCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<SubjectDto>> GetAll([FromQuery] GetAllSubjectsCourseQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<SubjectDto> GetById([FromRoute] Guid id, [FromQuery] GetSubjectByIdQuery query)
        {
            query.Id = id;
            return await this.Mediator.Send(query);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Statics.SchoolClaim)]
        public async Task<SubjectDto> Update([FromRoute] Guid id, [FromBody] UpdateSubjectCommand command)
        {
            command.Id = id;
            return await this.Mediator.Send(command);
        }
    }
}
