namespace NetPower.Domain.Entities.Common;

/// <summary>
/// Base entity class that all domain entities inherit from.
/// Provides the common Id property required for all entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }
}
