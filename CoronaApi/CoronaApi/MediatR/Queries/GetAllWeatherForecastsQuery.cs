using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CoronaApi.MediatR.Queries
{
    public class GetAllWeatherForecastsQuery : IRequest<List<WeatherForecast>>
    {
        public class GetAllWeatherForecastsQueryHandler : IRequestHandler<GetAllWeatherForecastsQuery, List<WeatherForecast>>
        {
            public Task<List<WeatherForecast>> Handle(GetAllWeatherForecastsQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(WeatherForecastDatabase.Instance);
            }
        }
    }

    public class WeatherForecastDatabase
    {
        public static readonly List<WeatherForecast> Instance = new Lazy<List<WeatherForecast>>(() =>
        {
            var rng = new Random();
            return Enumerable.Range(1, 5)
                             .Select(index => new WeatherForecast
                             {
                                 Date = DateTime.Now.AddDays(index),
                                 TemperatureC = rng.Next(-20, 55)
                             })
                             .ToList();
        }).Value;
    }
}