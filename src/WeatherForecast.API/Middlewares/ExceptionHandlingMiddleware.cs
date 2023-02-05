namespace WeatherForecast.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

         public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled app exception");
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Unhandled error");
            }
            
        }
    }
}