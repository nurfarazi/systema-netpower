using Microsoft.AspNetCore.Mvc;
using NetPower.Application.Features.WeatherForecasts.DTOs;
using NetPower.Application.Features.WeatherForecasts.Services;
using NetPower.Domain.Exceptions;

namespace NetPower.API.Controllers;

/// <summary>
/// API controller for managing weather forecasts.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Weather")]
public class WeatherForecastsController : ApiControllerBase
{
    private readonly IWeatherForecastService _service;
    private readonly ILogger<WeatherForecastsController> _logger;

    public WeatherForecastsController(
        IWeatherForecastService service,
        ILogger<WeatherForecastsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Gets all weather forecasts.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all weather forecasts</returns>
    /// <response code="200">Returns the list of weather forecasts</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var forecasts = await _service.GetAllAsync(cancellationToken);
            return Ok(forecasts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving weather forecasts");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving weather forecasts.");
        }
    }

    /// <summary>
    /// Gets a specific weather forecast by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the weather forecast</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The weather forecast details</returns>
    /// <response code="200">Returns the weather forecast</response>
    /// <response code="404">If the weather forecast is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    public async Task<ActionResult<WeatherForecastDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var forecast = await _service.GetByIdAsync(id, cancellationToken);
            if (forecast == null)
            {
                return NotFound();
            }
            return Ok(forecast);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving weather forecast with ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the weather forecast.");
        }
    }

    /// <summary>
    /// Creates a new weather forecast.
    /// </summary>
    /// <param name="dto">The weather forecast creation data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The ID of the created weather forecast</returns>
    /// <response code="201">Returns the newly created weather forecast ID</response>
    /// <response code="400">If the weather forecast data is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<int>> Create(CreateWeatherForecastDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Validation error creating weather forecast");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating weather forecast");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the weather forecast.");
        }
    }

    /// <summary>
    /// Updates an existing weather forecast.
    /// </summary>
    /// <param name="id">The unique identifier of the weather forecast to update</param>
    /// <param name="dto">The updated weather forecast data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>No content</returns>
    /// <response code="204">If the weather forecast was successfully updated</response>
    /// <response code="404">If the weather forecast is not found</response>
    /// <response code="400">If the weather forecast data is invalid</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Consumes("application/json")]
    public async Task<IActionResult> Update(int id, CreateWeatherForecastDto dto, CancellationToken cancellationToken)
    {
        try
        {
            await _service.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogWarning(ex, "Weather forecast not found with ID {Id}", id);
            return NotFound();
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Validation error updating weather forecast");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating weather forecast with ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the weather forecast.");
        }
    }

    /// <summary>
    /// Deletes a weather forecast.
    /// </summary>
    /// <param name="id">The unique identifier of the weather forecast to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>No content</returns>
    /// <response code="204">If the weather forecast was successfully deleted</response>
    /// <response code="404">If the weather forecast is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogWarning(ex, "Weather forecast not found with ID {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting weather forecast with ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the weather forecast.");
        }
    }
}
