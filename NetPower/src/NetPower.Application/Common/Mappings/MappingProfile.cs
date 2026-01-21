using AutoMapper;
using NetPower.Application.Features.Users.DTOs;
using NetPower.Application.Features.WeatherForecasts.DTOs;
using NetPower.Domain.Entities;

namespace NetPower.Application.Common.Mappings;

/// <summary>
/// AutoMapper configuration for all entity-to-DTO mappings.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WeatherForecast, WeatherForecastDto>();
        CreateMap<CreateWeatherForecastDto, WeatherForecast>();

        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
    }
}
