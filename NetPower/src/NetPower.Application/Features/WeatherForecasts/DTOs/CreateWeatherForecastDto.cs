namespace NetPower.Application.Features.WeatherForecasts.DTOs;

/// <summary>
/// DTO for creating a new weather forecast.
/// </summary>
public class CreateWeatherForecastDto
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
    /// Weather summary.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Location of the forecast.
    /// </summary>
    public string? Location { get; set; }
}
