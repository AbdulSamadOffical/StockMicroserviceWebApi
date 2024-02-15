using Microsoft.Extensions.Logging;
using Stock.Domain.Interfaces.MessageBroker;
using Microsoft.Extensions.Hosting;
using Stock.Domain.Dtos;

namespace Stock.Infrastructure.MessageBroker.rabbitmq
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _busControl;
        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _busControl = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _busControl.ReceiveAsync<StockDto>("stock", x =>
            {
                Task.Run(() => { DidJob(x); }, stoppingToken);
            });
        }

        private void DidJob(StockDto stock)
        {

            _logger.LogInformation($"CompanyName: {stock.CompanyName}, Stock Symbol: {stock.Symbol}");

        }
    }
}
