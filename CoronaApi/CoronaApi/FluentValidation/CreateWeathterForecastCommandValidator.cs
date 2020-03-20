using CoronaApi.MediatR.Commands;
using FluentValidation;

namespace CoronaApi.FluentValidation
{
    public class CreateWeathterForecastCommandValidator : AbstractValidator<CreateWeathterForecastCommand>
    {
        public CreateWeathterForecastCommandValidator()
        {
            this.RuleFor(forecast => forecast.TemperatureC)
                .NotEmpty();
            this.RuleFor(forecast => forecast.Date)
                .NotEmpty();
        }
    }
}