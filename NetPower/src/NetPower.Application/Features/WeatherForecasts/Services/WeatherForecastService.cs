using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NetPower.Application.Common.Interfaces;
using NetPower.Application.Features.WeatherForecasts.DTOs;
using NetPower.Application.Features.WeatherForecasts.Validators;
using NetPower.Domain.Entities;
using NetPower.Domain.Exceptions;

namespace NetPower.Application.Features.WeatherForecasts.Services;

/// <summary>
/// Service for managing weather forecast operations.
/// </summary>
public class WeatherForecastService : IWeatherForecastService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly CreateWeatherForecastValidator _validator;

    public WeatherForecastService(
        IApplicationDbContext dbContext,
        IMapper mapper,
        CreateWeatherForecastValidator validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IEnumerable<WeatherForecastDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var forecasts = await _dbContext.WeatherForecasts.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<WeatherForecastDto>>(forecasts);
    }

    public async Task<WeatherForecastDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var forecast = await _dbContext.WeatherForecasts
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        return forecast == null ? null : _mapper.Map<WeatherForecastDto>(forecast);
    }

    public async Task<int> CreateAsync(CreateWeatherForecastDto dto, CancellationToken cancellationToken = default)
    {
        // Validate input
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        // Map DTO to entity
        var forecast = _mapper.Map<WeatherForecast>(dto);
        forecast.CreatedAt = DateTime.UtcNow;
        forecast.CreatedBy = "System"; // This could be replaced with actual user info

        // Add to database
        _dbContext.WeatherForecasts.Add(forecast);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return forecast.Id;
    }

    public async Task UpdateAsync(int id, CreateWeatherForecastDto dto, CancellationToken cancellationToken = default)
    {
        // Validate input
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        // Get existing forecast
        var forecast = await _dbContext.WeatherForecasts
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(WeatherForecast), id);

        // Update properties
        forecast.Date = dto.Date;
        forecast.TemperatureC = dto.TemperatureC;
        forecast.Summary = dto.Summary;
        forecast.Location = dto.Location;
        forecast.ModifiedAt = DateTime.UtcNow;
        forecast.ModifiedBy = "System"; // This could be replaced with actual user info

        // Save changes
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var forecast = await _dbContext.WeatherForecasts
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(WeatherForecast), id);

        _dbContext.WeatherForecasts.Remove(forecast);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
