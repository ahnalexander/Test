using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using MyApp.Application.Common.Exceptions;

namespace MyApp.API.Middleware;

[JsonDerivedType(typeof(ErrorResponse), typeDiscriminator: "error")]
[JsonDerivedType(typeof(ValidationErrorResponse), typeDiscriminator: "validation")]
public abstract record ApiResponse(int StatusCode);
public record ErrorResponse(string Message, int StatusCode) : ApiResponse(StatusCode);
public record ValidationError(string Property, string Error);
public record ValidationErrorResponse(string Message, IEnumerable<ValidationError> Errors, int StatusCode) 
    : ApiResponse(StatusCode);

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

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
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        
        ApiResponse result = exception switch
        {
            ValidationException validationEx => new ValidationErrorResponse(
                "Validation failed",
                validationEx.Errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage)),
                StatusCodes.Status400BadRequest),
            NotFoundException => new ErrorResponse(
                exception.Message,
                StatusCodes.Status404NotFound),
            UnauthorizedException => new ErrorResponse(
                exception.Message,
                StatusCodes.Status401Unauthorized),
            _ => new ErrorResponse(
                exception.Message,
                StatusCodes.Status500InternalServerError)
        };

        response.StatusCode = result.StatusCode;
        await response.WriteAsync(JsonSerializer.Serialize(result, JsonOptions));
    }
}