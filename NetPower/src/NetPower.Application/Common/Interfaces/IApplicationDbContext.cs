using Microsoft.EntityFrameworkCore;
using NetPower.Domain.Entities;

namespace NetPower.Application.Common.Interfaces;

/// <summary>
/// Application database context interface for dependency injection.
/// </summary>
public interface IApplicationDbContext
{
    DbSet<WeatherForecast> WeatherForecasts { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
