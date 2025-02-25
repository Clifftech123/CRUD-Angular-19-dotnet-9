using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static API.Infrastructure.Exceptions.CustomExceptions;

namespace API.Infrastructure.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = GetStatusCode(exception),
                Title = GetTitle(exception),
                Detail = exception.Message,
                Instance = httpContext.Request.Path,
                Type = exception.GetType().Name
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
            problemDetails.Extensions.Add("timestamp", DateTime.UtcNow);

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private static int GetStatusCode(Exception exception) => exception switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            ProductNotFoundException => StatusCodes.Status404NotFound,
            InvalidProductPriceException => StatusCodes.Status400BadRequest,
            DuplicateProductNameException => StatusCodes.Status409Conflict,
            InvalidProductDataException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        private static string GetTitle(Exception exception) => exception switch
        {
            BadHttpRequestException => "Bad Request",
            ValidationException => "Validation Error",
            ArgumentException => "Invalid Arguments",
            InvalidOperationException => "Invalid Operation",
            ProductNotFoundException => "Product Not Found",
            InvalidProductPriceException => "Invalid Product Price",
            DuplicateProductNameException => "Duplicate Product Name",
            InvalidProductDataException => "Invalid Product Data",
            _ => "Internal Server Error"
        };
    }
}
