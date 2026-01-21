namespace NetPower.Domain.Exceptions;

/// <summary>
/// Base exception for domain-level errors.
/// Used for business logic violations and domain rule breaches.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the DomainException class.
    /// </summary>
    public DomainException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the DomainException class with an inner exception.
    /// </summary>
    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
