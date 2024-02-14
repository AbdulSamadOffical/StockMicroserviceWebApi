using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Stock.Infrastructure.Persistence;
using Stock.Domain.RepositoryContracts;
using Stock.Infrastructure.Persistence.Repository;
using Stock.Domain.Interfaces;
using Stock.Application.AppUsecases.Stocks.GetStocks;
using Stock.Application.AppUsecases.Stocks.CreateStocks;
using Stock.Application.AppUsecases.Stocks.UpdateStock;
using Stock.Application.AppUsecases.Stocks.DeleteStock;


public static class Startup
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            , b => b.MigrationsAssembly("StockApi")));

      
        services.AddScoped(typeof(IGenericRepository<>), typeof(InMemoryGenericRepository<>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IStockProductRepository, StockProductRepository>();
        services.AddScoped<GetStocksUseCase>();
        services.AddScoped<CreateStockUsecase>();
        services.AddScoped<UpdateStockUseCase>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DeleteStockUsecase>();
       
    }

}