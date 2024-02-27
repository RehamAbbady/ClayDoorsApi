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
                await _next(httpContext); // Call the next middleware in the pipeline
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
            context.Response.StatusCode = StatusCodes.Status500InternalServerError; 

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = $"Internal Server Error {exception.Message}" 
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
