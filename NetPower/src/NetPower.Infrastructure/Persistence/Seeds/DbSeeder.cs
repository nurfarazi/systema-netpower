using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetPower.Domain.Entities;
using NetPower.Infrastructure.Persistence.Contexts;

namespace NetPower.Infrastructure.Persistence.Seeds;

/// <summary>
/// Database seeder for initializing test data.
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Seeds the database with initial test data.
    /// </summary>
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

        try
        {
            // Ensure database is created and migrations are applied
            await context.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied successfully.");

            // Seed Users
            await SeedUsersAsync(context, logger);

            logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    /// <summary>
    /// Seeds 40 test users into the database.
    /// </summary>
    private static async Task SeedUsersAsync(ApplicationDbContext context, ILogger logger)
    {
        // Check if users already exist
        if (await context.Users.AnyAsync())
        {
            logger.LogInformation("Users already exist in database. Skipping user seeding.");
            return;
        }

        logger.LogInformation("Seeding 40 users into the database...");

        var users = new List<User>();
        var random = new Random(42); // Fixed seed for reproducible data
        var now = DateTime.UtcNow;

        var firstNames = new[]
        {
            "James", "Mary", "John", "Patricia", "Robert", "Jennifer", "Michael", "Linda",
            "William", "Barbara", "David", "Elizabeth", "Richard", "Susan", "Joseph", "Jessica",
            "Thomas", "Sarah", "Christopher", "Karen", "Charles", "Nancy", "Daniel", "Lisa",
            "Matthew", "Betty", "Anthony", "Margaret", "Mark", "Sandra", "Donald", "Ashley",
            "Steven", "Emily", "Andrew", "Kimberly", "Paul", "Donna", "Joshua", "Michelle"
        };

        var lastNames = new[]
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis",
            "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas",
            "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White",
            "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young",
            "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores"
        };

        var domains = new[] { "example.com", "test.com", "demo.com", "sample.com", "email.com" };

        for (int i = 0; i < 40; i++)
        {
            var firstName = firstNames[i % firstNames.Length];
            var lastName = lastNames[i % lastNames.Length];
            var fullName = $"{firstName} {lastName}";
            var email = $"{firstName.ToLower()}.{lastName.ToLower()}{(i / firstNames.Length > 0 ? (i / firstNames.Length).ToString() : "")}@{domains[i % domains.Length]}";
            var isActive = i % 5 != 0; // 80% active, 20% inactive
            var createdDaysAgo = random.Next(1, 180); // Created within last 180 days

            var user = new User
            {
                Name = fullName,
                Email = email,
                IsActive = isActive,
                CreatedAt = now.AddDays(-createdDaysAgo),
                CreatedBy = "System",
                ModifiedAt = random.Next(0, 2) == 0 ? now.AddDays(-random.Next(0, createdDaysAgo)) : null,
                ModifiedBy = random.Next(0, 2) == 0 ? "System" : null
            };

            users.Add(user);
        }

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();

        logger.LogInformation("Successfully seeded {Count} users into the database.", users.Count);
    }
}
