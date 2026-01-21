using System.Text.Json;
using NetPower.Domain.Exceptions;

namespace NetPower.API.Middleware;

/// <summary>
/// Middleware for handling exceptions and returning appropriate HTTP responses.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred while executing the request.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse();

        switch (exception)
        {
            case EntityNotFoundException ex:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status404NotFound;
                break;

            case DomainException ex:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "An internal server error occurred.";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        return context.Response.WriteAsJsonAsync(response);
    }

    private class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
