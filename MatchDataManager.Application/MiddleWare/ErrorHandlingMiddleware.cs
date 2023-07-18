using MatchDataManager.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MatchDataManager.Application.MiddleWare
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadHttpRequestException notFoundException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Somthing went wrong");
            }
        }
    }
}