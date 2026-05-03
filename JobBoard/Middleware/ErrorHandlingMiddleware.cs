using System.Net;
using System.Text.Json;

namespace JobBoard.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = ex.Message switch
            {
                var m when m.Contains("not found") => HttpStatusCode.NotFound,
                var m when m.Contains("Unauthorized") => HttpStatusCode.Forbidden,
                var m when m.Contains("already") => HttpStatusCode.Conflict,
                var m when m.Contains("not open") => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new { message = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
