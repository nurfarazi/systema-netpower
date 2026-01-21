using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetPower.Domain.Entities;
using NetPower.Domain.Interfaces.Repositories;
using NetPower.Infrastructure.Persistence.Contexts;

namespace NetPower.Infrastructure.Persistence.Repositories.EfCore;

/// <summary>
/// Repository implementation for User entities.
/// Uses EF Core for data queries and ADO.NET for optimized count operations.
/// </summary>
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(ApplicationDbContext context, IConfiguration configuration)
        : base(context)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<IEnumerable<User>> GetPagedAsync(
        string? search,
        bool? isActive,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(u =>
                u.Name.ToLower().Contains(searchLower) ||
                u.Email.ToLower().Contains(searchLower));
        }

        if (isActive.HasValue)
        {
            query = query.Where(u => u.IsActive == isActive.Value);
        }

        // Apply pagination
        var users = await query
            .OrderBy(u => u.Name)  // Consistent ordering for pagination
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return users;
    }

    public async Task<int> GetCountAsync(
        string? search,
        bool? isActive,
        CancellationToken cancellationToken = default)
    {
        // Use ADO.NET with raw SQL for optimized count query
        // This demonstrates how to use ADO.NET alongside EF Core
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        // Build dynamic SQL with parameterized queries to prevent SQL injection
        var sql = "SELECT COUNT(*) FROM Users WHERE 1=1";
        var parameters = new List<SqlParameter>();

        if (!string.IsNullOrWhiteSpace(search))
        {
            sql += " AND (LOWER(Name) LIKE @Search OR LOWER(Email) LIKE @Search)";
            parameters.Add(new SqlParameter("@Search", $"%{search.ToLower()}%"));
        }

        if (isActive.HasValue)
        {
            sql += " AND IsActive = @IsActive";
            parameters.Add(new SqlParameter("@IsActive", isActive.Value));
        }

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddRange(parameters.ToArray());

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt32(result);
    }
}
