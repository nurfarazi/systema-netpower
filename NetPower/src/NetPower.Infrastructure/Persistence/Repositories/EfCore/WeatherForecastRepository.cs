using Microsoft.EntityFrameworkCore;
using NetPower.Domain.Entities;
using NetPower.Domain.Interfaces.Repositories;
using NetPower.Infrastructure.Persistence.Contexts;

namespace NetPower.Infrastructure.Persistence.Repositories.EfCore;

/// <summary>
/// Repository implementation for WeatherForecast entities using Entity Framework Core.
/// </summary>
public class WeatherForecastRepository : GenericRepository<WeatherForecast>, IWeatherForecastRepository
{
    public WeatherForecastRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WeatherForecast>> GetByLocationAsync(string location, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(w => w.Location == location)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<WeatherForecast>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(w => w.Date >= startDate && w.Date <= endDate)
            .OrderBy(w => w.Date)
            .ToListAsync(cancellationToken);
    }
}
