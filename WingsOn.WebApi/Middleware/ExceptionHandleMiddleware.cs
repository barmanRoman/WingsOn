using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WingsOn.WebApi.Exceptions;

namespace WingsOn.WebApi.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandleMiddleware> _logger;

        private readonly Dictionary<Type, int> _exceptionDictionary = new Dictionary<Type, int>()
        {
            {typeof(DomainObjectNotFoundException), 404},
            {typeof(ValidationException), 400}
        };

        public ExceptionHandleMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<ExceptionHandleMiddleware>() ??
                      throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                context.Response.Clear();
                if (_exceptionDictionary.ContainsKey(exception.GetType()))
                {
                    _logger.LogWarning(exception,string.Empty);
                    context.Response.StatusCode = _exceptionDictionary[exception.GetType()];
                }
                else
                {
                    _logger.LogError(exception, string.Empty);
                    context.Response.StatusCode = 500;
                }
                context.Response.ContentType = @"text/plain";
                await context.Response.WriteAsync(exception.Message);
            }
        }
    }

    public static class ExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }
}