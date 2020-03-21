using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaApi.BackgroundServices.Channels;
using CoronaApi.MediatR.Commands;
using CoronaApi.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoronaApi.Controllers
{
    public class WeatherForecastController : BaseV1ApiController
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "School")]
        public async Task<IEnumerable<WeatherForecast>> Get([FromQuery]GetAllWeatherForecastsQuery query)
        {
            return await this._mediator.Send(query)
                             .ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<WeatherForecast> Post(CreateWeathterForecastCommand command)
        {
            return await this._mediator.Send(command)
                             .ConfigureAwait(false);
        }
    }
}