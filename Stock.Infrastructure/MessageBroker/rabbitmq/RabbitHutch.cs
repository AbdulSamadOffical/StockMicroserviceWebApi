using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Stock.Domain.Interfaces.MessageBroker;
using Stock.Infrastructure.MessageBroker.rabbitmq;


namespace Stock.Infrastructure.MessageBroker
{
    public class RabbitHutch
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _channel;
        public static IBus CreateBus(string hostName, ILogger _logger)
        {
            _factory = new ConnectionFactory
            {
                HostName = hostName,
                DispatchConsumersAsync = true
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
          
            return new RabbitBus(_channel, _logger);
        }
    }
}
