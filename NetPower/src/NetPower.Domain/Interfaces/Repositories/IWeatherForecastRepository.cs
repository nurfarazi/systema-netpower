using NetPower.Domain.Entities;

namespace NetPower.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for WeatherForecast entities.
/// Extends the generic repository with domain-specific query methods.
/// </summary>
public interface IWeatherForecastRepository : IGenericRepository<WeatherForecast>
{
    /// <summary>
    /// Gets all weather forecasts for a specific location.
    /// </summary>
    Task<IEnumerable<WeatherForecast>> GetByLocationAsync(string location, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets weather forecasts within a date range.
    /// </summary>
    Task<IEnumerable<WeatherForecast>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}
