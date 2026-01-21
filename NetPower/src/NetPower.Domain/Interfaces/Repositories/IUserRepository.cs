using NetPower.Domain.Entities;

namespace NetPower.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for User entities.
/// Extends the generic repository with domain-specific query methods.
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
    /// <summary>
    /// Gets a paginated list of users with optional filtering.
    /// </summary>
    /// <param name="search">Search term for name or email</param>
    /// <param name="isActive">Filter by active status</param>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of users for the requested page</returns>
    Task<IEnumerable<User>> GetPagedAsync(
        string? search,
        bool? isActive,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the total count of users matching the filter criteria using raw SQL (ADO.NET).
    /// This demonstrates an optimized count query without loading entities.
    /// </summary>
    /// <param name="search">Search term for name or email</param>
    /// <param name="isActive">Filter by active status</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Total count of matching users</returns>
    Task<int> GetCountAsync(
        string? search,
        bool? isActive,
        CancellationToken cancellationToken = default);
}
