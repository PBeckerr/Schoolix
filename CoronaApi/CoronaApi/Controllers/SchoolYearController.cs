using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaApi.Dtos;
using CoronaApi.MediatR.SchoolYear.Commands;
using CoronaApi.MediatR.SchoolYear.Queries;
using CoronaApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoronaApi.Controllers
{
    public class SchoolYearController : BaseV1ApiController
    {
        [HttpPost]
        public async Task<SchoolYearDto> Create([FromQuery]CreateSchoolYearCommand command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpGet]
        public async Task<List<SchoolYearDto>> GetAll([FromQuery]GetAllSchoolYearsQuery query)
        {
            return await Mediator.Send(query);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<SchoolYearDto> GetById([FromQuery]GetSchoolYearByIdQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
