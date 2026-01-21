using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetPower.Application.Common.Interfaces;
using NetPower.Domain.Interfaces.Repositories;
using NetPower.Infrastructure.Persistence.Contexts;
using NetPower.Infrastructure.Persistence.Repositories.EfCore;

namespace NetPower.Infrastructure;

/// <summary>
/// Extension methods for registering infrastructure layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure layer services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="useAdoNet">Whether to use ADO.NET repositories instead of EF Core (optional).</param>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool useAdoNet = false)
    {
        // Register DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionString 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register ApplicationDbContext interface
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Register Repositories
        if (!useAdoNet)
        {
            // Use Entity Framework Core repositories
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        return services;
    }
}
