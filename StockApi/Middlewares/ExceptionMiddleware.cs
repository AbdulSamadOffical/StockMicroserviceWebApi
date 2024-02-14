using Stock.Domain.Exceptions;
using StockApi.Middlewares.Models;
using System.Net;

namespace StockApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
       
        public ExceptionMiddleware(RequestDelegate next)
        {
            
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, CustomException exception)
        {
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Stacktrace = exception.StackTrace ?? "" // avoiding the null
            }.ToString());
        }
    }
}
