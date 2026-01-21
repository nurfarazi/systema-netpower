namespace NetPower.Application.Features.WeatherForecasts.DTOs;

/// <summary>
/// Data transfer object for WeatherForecast.
/// </summary>
public class WeatherForecastDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The date of the forecast.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Temperature in Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF { get; set; }

    /// <summary>
    /// Weather summary.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Location of the forecast.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// When the record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Who created the record.
    /// </summary>
    public string? CreatedBy { get; set; }
}
