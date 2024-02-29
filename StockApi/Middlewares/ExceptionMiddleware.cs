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
            catch (Exception ex)
            {
                
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
           

            if (exception is BadRequestException)
            {
                context.Response.StatusCode = 401;
            }
            else if (exception is NotFoundException)
            {
                context.Response.StatusCode = 404;
            }
            else
            {
                context.Response.StatusCode = 500;
            }



            var errorResponse = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,

            };
            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(errorResponse));
        }
    }
}
