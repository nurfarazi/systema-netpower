using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPower.Domain.Entities;

namespace NetPower.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the User entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.CreatedBy)
            .HasMaxLength(100);

        builder.Property(u => u.ModifiedAt)
            .IsRequired(false);

        builder.Property(u => u.ModifiedBy)
            .HasMaxLength(100);

        // Create indexes for query performance
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.Name);

        builder.HasIndex(u => u.IsActive);

        // Composite index for optimized search with isActive filter
        // This index improves performance when filtering by IsActive and searching by Name
        builder.HasIndex(u => new { u.IsActive, u.Name })
            .HasDatabaseName("IX_Users_IsActive_Name");

        builder.HasIndex(u => new { u.IsActive, u.Email })
            .HasDatabaseName("IX_Users_IsActive_Email");

        builder.ToTable("Users");
    }
}
