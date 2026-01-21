namespace NetPower.Domain.Common;

/// <summary>
/// Represents the result of an operation, either success or failure.
/// Provides type-safe error handling without relying on exceptions.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// Gets the error message if the operation failed.
    /// </summary>
    public string? Error { get; private set; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success() => new() { IsSuccess = true };

    /// <summary>
    /// Creates a failed result with an error message.
    /// </summary>
    public static Result Failure(string error) => new() { IsSuccess = false, Error = error };
}

/// <summary>
/// Represents the result of an operation with a return value, either success or failure.
/// </summary>
/// <typeparam name="T">The type of the result value.</typeparam>
public class Result<T>
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// Gets the result value if the operation was successful.
    /// </summary>
    public T? Value { get; private set; }

    /// <summary>
    /// Gets the error message if the operation failed.
    /// </summary>
    public string? Error { get; private set; }

    /// <summary>
    /// Creates a successful result with a value.
    /// </summary>
    public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };

    /// <summary>
    /// Creates a failed result with an error message.
    /// </summary>
    public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
}
