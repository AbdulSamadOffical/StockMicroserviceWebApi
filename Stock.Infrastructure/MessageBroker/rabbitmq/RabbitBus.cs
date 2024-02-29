using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Stock.Domain.Interfaces.MessageBroker;
using System.Text;


namespace Stock.Infrastructure.MessageBroker.rabbitmq
{
    public class RabbitBus : IBus
    {
        private readonly IModel _channel;
        private readonly ILogger _logger;
      
        internal RabbitBus(IModel channel, ILogger logger)
        {
            _channel = channel;
            _logger = logger;
        }
        public async Task SendAsync<T>(string exchange, T message)
        {
            try
            {
                // Declaring a queue and storing the message in that queue until it's consumed.
                /*
                await Task.Run(() =>
                {
                    _channel.QueueDeclare(queue, true, false, false);
                    var properties = _channel.CreateBasicProperties();
                    properties.Persistent = false;
                    var output = JsonConvert.SerializeObject(message);
                    _channel.BasicPublish(string.Empty, queue, null,
                    Encoding.UTF8.GetBytes(output));
                });
               */

                // Fanout is used to broad cast message to multiple consumers.
                await Task.Run(() =>
                {
                    _channel.ExchangeDeclare(exchange, ExchangeType.Fanout, true);
                    var properties = _channel.CreateBasicProperties();
                    properties.Persistent = false;
                    var output = JsonConvert.SerializeObject(message);
                    _channel.BasicPublish(exchange, string.Empty, null,
                        Encoding.UTF8.GetBytes(output));
                });
            }
            catch (Exception ex)
            {            
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);

            }
            
        }
        public async Task ReceiveAsync<T>(string exchange, string queue, Action<T> onMessage)
        {
            try
            {
                _channel.ExchangeDeclare(exchange, ExchangeType.Fanout, true);
                _channel.QueueDeclare(queue, true, false, false);
                _channel.QueueBind(queue, exchange, "");
                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.Received += async (s, e) =>
                {
                    var jsonSpecified = Encoding.UTF8.GetString(e.Body.Span);
                    var item = JsonConvert.DeserializeObject<T>(jsonSpecified);
                    onMessage(item);
                    await Task.Yield();
                };

                _channel.BasicConsume(queue, true, consumer);
                await Task.Yield();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            }
            
        }
    }
}
