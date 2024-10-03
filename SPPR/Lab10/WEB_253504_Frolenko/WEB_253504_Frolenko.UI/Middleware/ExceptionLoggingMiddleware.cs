using Microsoft.AspNetCore.Http;
using Serilog;
using System.Threading.Tasks;

namespace WEB_253504_Frolenko.UI.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLoggingMiddleware> _logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode < 200 || context.Response.StatusCode >= 300)
                {
                    _logger.LogInformation("---> request {Url} returns {StatusCode}", context.Request.Path, context.Response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке запроса {Url}", context.Request.Path);

                throw;
            }
        }
    }
}