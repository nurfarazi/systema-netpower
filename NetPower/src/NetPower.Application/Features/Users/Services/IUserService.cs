using NetPower.Application.Common.Models;
using NetPower.Application.Features.Users.DTOs;

namespace NetPower.Application.Features.Users.Services;

/// <summary>
/// Service interface for user operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets a paginated list of users with optional filtering.
    /// </summary>
    Task<PagedResult<UserDto>> GetUsersAsync(
        GetUsersQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    Task<int> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default);
}
