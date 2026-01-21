using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NetPower.Application.Common.Mappings;
using NetPower.Application.Features.Users.Services;
using NetPower.Application.Features.Users.Validators;
using NetPower.Application.Features.WeatherForecasts.Services;
using NetPower.Application.Features.WeatherForecasts.Validators;

namespace NetPower.Application;

/// <summary>
/// Extension methods for registering application layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application layer services to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // Register Validators
        services.AddScoped<CreateWeatherForecastValidator>();
        services.AddScoped<GetUsersQueryValidator>();
        services.AddScoped<CreateUserValidator>();

        // Register Application Services
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        services.AddScoped<IUserService, UserService>();

        // Register FluentValidation
        services.AddValidatorsFromAssemblyContaining(typeof(CreateWeatherForecastValidator));

        return services;
    }
}
