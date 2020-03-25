using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Schoolix.Dtos;
using Schoolix.MediatR.Submission.Commands;
using Schoolix.MediatR.Submission.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Schoolix.Controllers
{
    public class SubmissionController : BaseV1ApiController
    {
        [HttpPost]
        public async Task<SubmissionDto> Post([FromForm] CreateSubmissionCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<SubmissionDto>> GetAll([FromQuery]GetAllSubmissionsQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<SubmissionDto> GetById([FromRoute]GetSubmissionByIdQuery query)
        {
            return await this.Mediator.Send(query);
        }

        [HttpPut("{id}")]
        public async Task<SubmissionDto> Put([FromRoute] Guid id, [FromForm] UpdateSubmissionCommand query)
        {
            query.Id = id;
            return await this.Mediator.Send(query);
        }
    }
}