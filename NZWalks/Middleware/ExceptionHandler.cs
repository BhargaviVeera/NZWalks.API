using System.Net;

namespace NZWalks.API.Middleware
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly RequestDelegate _request;

        public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate request)
        {
            _logger = logger;
            _request = request;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _request(httpContext);
            }
            catch (Exception e)
            {
                var errid = Guid.NewGuid();
                // Log the exception
                _logger.LogError(e, $"{errid} : {e.Message}");

                // Return a custom error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errid,
                    Message = "Something went wrong. Please be patient as our team is looking into it...",
                    Details = e.Message // optional: for debugging in development
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
