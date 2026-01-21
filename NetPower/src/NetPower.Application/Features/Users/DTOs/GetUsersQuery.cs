namespace NetPower.Application.Features.Users.DTOs;

/// <summary>
/// Query parameters for retrieving users with filtering and pagination.
/// </summary>
public class GetUsersQuery
{
    /// <summary>
    /// Search term to filter by name or email.
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// Filter by active status. Null returns all users.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Page number (1-based). Defaults to 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page. Defaults to 10.
    /// </summary>
    public int PageSize { get; set; } = 10;
}
