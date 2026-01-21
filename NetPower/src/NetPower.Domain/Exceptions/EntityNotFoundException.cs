namespace NetPower.Domain.Exceptions;

/// <summary>
/// Exception thrown when an entity is not found in the data store.
/// </summary>
public class EntityNotFoundException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the EntityNotFoundException class.
    /// </summary>
    public EntityNotFoundException(string entityName, object key)
        : base($"Entity '{entityName}' with key '{key}' was not found.")
    {
    }
}
