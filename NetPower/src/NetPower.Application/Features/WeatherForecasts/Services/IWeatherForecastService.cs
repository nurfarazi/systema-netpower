using NetPower.Application.Features.WeatherForecasts.DTOs;

namespace NetPower.Application.Features.WeatherForecasts.Services;

/// <summary>
/// Service interface for weather forecast operations.
/// </summary>
public interface IWeatherForecastService
{
    /// <summary>
    /// Gets all weather forecasts.
    /// </summary>
    Task<IEnumerable<WeatherForecastDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a weather forecast by ID.
    /// </summary>
    Task<WeatherForecastDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new weather forecast.
    /// </summary>
    Task<int> CreateAsync(CreateWeatherForecastDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing weather forecast.
    /// </summary>
    Task UpdateAsync(int id, CreateWeatherForecastDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a weather forecast.
    /// </summary>
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
