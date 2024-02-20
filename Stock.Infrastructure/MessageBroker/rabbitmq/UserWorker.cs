using Microsoft.Extensions.Logging;
using Stock.Domain.Interfaces.MessageBroker;
using Microsoft.Extensions.Hosting;
using Stock.Domain.Dtos;
using Stock.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Infrastructure.MessageBroker.rabbitmq
{
    public class UserWorker : BackgroundService
    {
        private readonly ILogger<UserWorker> _logger;
        private readonly IBus _busControl;
        private readonly IServiceProvider _serviceProvider;

        public UserWorker(ILogger<UserWorker> logger, IBus bus, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _busControl = bus;
            _serviceProvider = serviceProvider;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _busControl.ReceiveAsync<UserDto>("user", x =>
            {
                Task.Run(() => { DidJob(x); }, stoppingToken);
            });
        }

        private void DidJob(UserDto user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                
                scopedService.UserRepository.CreateUser(user);
                scopedService.Complete();
                _logger.LogInformation($"CompanyName: {user.UserName}, Stock Symbol: {user.Email}");
            }
           

        }
    }
}
