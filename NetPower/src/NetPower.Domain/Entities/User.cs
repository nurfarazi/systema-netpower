using NetPower.Domain.Entities.Common;

namespace NetPower.Domain.Entities;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User : AuditableEntity, IAggregateRoot
{
    /// <summary>
    /// The user's full name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the user is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
