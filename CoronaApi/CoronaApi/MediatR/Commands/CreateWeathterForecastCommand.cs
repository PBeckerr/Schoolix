using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Mapping;
using CoronaApi.MediatR.Queries;
using MediatR;

namespace CoronaApi.MediatR.Commands
{
    public class CreateWeathterForecastCommand : IRequest<WeatherForecast>, IMapFrom<WeatherForecast>
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }

        public class CreateWeathterForecastCommandHandler : IRequestHandler<CreateWeathterForecastCommand, WeatherForecast>
        {
            private readonly IMapper _mapper;

            public CreateWeathterForecastCommandHandler(IMapper mapper)
            {
                this._mapper = mapper;
            }

            public Task<WeatherForecast> Handle(CreateWeathterForecastCommand request, CancellationToken cancellationToken)
            {
                var mapped = this._mapper.Map<WeatherForecast>(request);
                WeatherForecastDatabase.Instance.Add(mapped);
                return Task.FromResult(mapped);
            }
        }
    }
}