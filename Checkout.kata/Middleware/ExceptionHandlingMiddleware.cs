using Checkout.kata.Domain.Models;
using System.Net;
using System.Text.Json;

namespace Checkout.kata.Middleware
{
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var clientMessage = "An unexpected error occurred.";

            if (exception is ArgumentException || exception is ArgumentOutOfRangeException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                clientMessage = exception.Message;
            }
            else if (exception is InvalidOperationException)
            {
                statusCode = (int)HttpStatusCode.Conflict;
                clientMessage = exception.Message;
            }

            var traceId = context.TraceIdentifier;
            var response = new ErrorResponse
            {
                Message = clientMessage,
                TraceId = traceId
            };

            _logger.LogError(exception, "Error occurred while processing request. TraceId={TraceId}", traceId);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await context.Response.WriteAsync(json);
        }
    }
}
