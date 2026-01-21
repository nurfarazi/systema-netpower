using NetPower.Domain.Entities.Common;

namespace NetPower.Domain.Entities;

/// <summary>
/// Represents a weather forecast for a specific date and location.
/// </summary>
public class WeatherForecast : AuditableEntity, IAggregateRoot
{
    /// <summary>
    /// The date of the forecast.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Temperature in Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Temperature in Fahrenheit, calculated from Celsius.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Summary description of the weather (e.g., "Sunny", "Rainy").
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// The location for which this forecast applies.
    /// </summary>
    public string? Location { get; set; }
}
