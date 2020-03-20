using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaApi.BackgroundServices.Channels;
using CoronaApi.MediatR.Commands;
using CoronaApi.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoronaApi.Controllers
{
    public class WeatherForecastController : BaseV1ApiController
    {
        private readonly FileProcessingChannel _fileProcessingChannel;
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator, FileProcessingChannel fileProcessingChannel)
        {
            this._fileProcessingChannel = fileProcessingChannel;
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get([FromQuery]GetAllWeatherForecastsQuery query)
        {
            await this._fileProcessingChannel.AddFileAsync(Guid.NewGuid().ToString());
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