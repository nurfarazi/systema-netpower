using FluentValidation;
using NetPower.Application.Features.WeatherForecasts.DTOs;

namespace NetPower.Application.Features.WeatherForecasts.Validators;

/// <summary>
/// Validator for CreateWeatherForecastDto.
/// </summary>
public class CreateWeatherForecastValidator : AbstractValidator<CreateWeatherForecastDto>
{
    public CreateWeatherForecastValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required.");

        RuleFor(x => x.TemperatureC)
            .GreaterThanOrEqualTo(-50)
            .LessThanOrEqualTo(60)
            .WithMessage("Temperature must be between -50 and 60 degrees Celsius.");

        RuleFor(x => x.Summary)
            .MaximumLength(100)
            .WithMessage("Summary must not exceed 100 characters.");

        RuleFor(x => x.Location)
            .MaximumLength(100)
            .WithMessage("Location must not exceed 100 characters.");
    }
}
