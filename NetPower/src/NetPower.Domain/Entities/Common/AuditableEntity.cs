namespace NetPower.Domain.Entities.Common;

/// <summary>
/// Base entity with automatic audit tracking fields.
/// Automatically populated by the infrastructure layer interceptors.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The user or system that created the entity.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// The date and time when the entity was last modified.
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// The user or system that last modified the entity.
    /// </summary>
    public string? ModifiedBy { get; set; }
}
