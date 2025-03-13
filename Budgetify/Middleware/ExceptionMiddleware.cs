using System.Net;
using System.Text.Json;
using Budgetify.Models;

namespace Budgetify.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            // Set status code and return the custom error response
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var errorResponse = new ApiResponse<string>(
                (int)HttpStatusCode.InternalServerError,
                "Unhandled exception occurred.",
                JsonSerializer.Serialize(new
                {
                    requestPath = httpContext.Request.Path,
                    headers = httpContext.Request.Headers
                }, new JsonSerializerOptions { WriteIndented = true })
            );

            await httpContext.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}