namespace CakeShop.Middleware
{
    public class LoggHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggHandlerMiddleware> _logger;

        public LoggHandlerMiddleware(RequestDelegate next, ILogger<LoggHandlerMiddleware> logger)
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
            finally
            {
                _logger.LogInformation(
                    "Requested method: {method}, with url: {url} return status code: {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode); 
            }
        }
    }
}
