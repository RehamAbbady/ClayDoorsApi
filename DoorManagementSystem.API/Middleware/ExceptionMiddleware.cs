using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace DoorManagementSystem.API.Middleware
{
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
                _logger.LogError(ex, $"An unhandled exception {ex.Message} has occurred at {ex.StackTrace}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Internal Server Error";
            var details = exception.Message;

            switch (exception)
            {
                case DbUpdateConcurrencyException:
                    statusCode = HttpStatusCode.Conflict;
                    message = "Database concurrency error occurred";
                    break;
                case DbUpdateException dbUpdateException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Database update error occurred";
                    details = dbUpdateException.InnerException?.Message ?? details;
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Validation errors occurred";
                    details = validationException.Message;
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Unauthorized access";
                    break;
                case NullReferenceException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Null reference exception occurred";
                    break;
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Argument errors occurred";
                    details = exception.Message;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = $"{message}: {details}"
            }.ToString());
        }

        private class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }

            public override string ToString() => JsonSerializer.Serialize(this);
        }
    }

}
