using StockApi.Middlewares;

namespace StockApi
{
    public static class Startup
    {
        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
