using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Schoolix.Dtos;
using Schoolix.MediatR.SchoolYear.Commands;
using Schoolix.MediatR.SchoolYear.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Schoolix.Controllers
{
    [Authorize]
    public class SchoolYearController : BaseV1ApiController
    {
        [HttpPost]
        [Authorize(Policy = Statics.SchoolClaim)]
        public async Task<SchoolYearDto> Create(CreateSchoolYearCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<SchoolYearDto>> GetAll([FromQuery] GetAllSchoolYearsQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<SchoolYearDto> GetById([FromRoute] GetSchoolYearByIdQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Statics.SchoolClaim)]
        public async Task<SchoolYearDto> Update([FromRoute] Guid id, [FromBody] UpdateSchoolYearCommand command)
        {
            command.Id = id;
            return await this.Mediator.Send(command);
        }
    }
}