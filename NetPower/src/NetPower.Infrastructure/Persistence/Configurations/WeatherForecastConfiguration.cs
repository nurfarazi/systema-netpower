using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPower.Domain.Entities;

namespace NetPower.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the WeatherForecast entity.
/// </summary>
public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .ValueGeneratedOnAdd();

        builder.Property(w => w.Date)
            .IsRequired();

        builder.Property(w => w.TemperatureC)
            .IsRequired();

        builder.Property(w => w.Summary)
            .HasMaxLength(100);

        builder.Property(w => w.Location)
            .HasMaxLength(100);

        builder.Property(w => w.CreatedAt)
            .IsRequired();

        builder.Property(w => w.CreatedBy)
            .HasMaxLength(100);

        builder.Property(w => w.ModifiedAt)
            .IsRequired(false);

        builder.Property(w => w.ModifiedBy)
            .HasMaxLength(100);

        // Create index on Date for better query performance
        builder.HasIndex(w => w.Date);

        // Create index on Location for filtering
        builder.HasIndex(w => w.Location);

        builder.ToTable("WeatherForecasts");
    }
}
