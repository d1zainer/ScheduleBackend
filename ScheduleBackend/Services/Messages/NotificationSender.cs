using Microsoft.AspNetCore.Connections;
using System.Text;
using System.Text.Json;
using ScheduleBackend.Models.Messages;
using ScheduleBackend.Services.Interfaces;
using RabbitMQ.Client;
using ScheduleBackend.Models.Settings;
using Microsoft.Extensions.Options;

namespace ScheduleBackend.Services.Messages
{
    public class NotificationSender : INotificationSender
    {


        private readonly ConnectionFactory _factory;
        private IConnection _connection;
        private IChannel _channel;
        private readonly RabbitMqSettings _rabbitMQSettings;

        private readonly ILogger _logger;
        private readonly string _exchangeName;
        private readonly string _routingKey;
        public NotificationSender(IOptions<RabbitMqSettings> options)
        {
            _rabbitMQSettings = options.Value;
            _factory = new ConnectionFactory { HostName = _rabbitMQSettings.HostName };
            _exchangeName = _rabbitMQSettings.ExchangeName;
            _routingKey = _rabbitMQSettings.RoutingKey;



            _initializeTask = new Lazy<Task>(async () =>
            {
                _connection = await _factory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();
                await _channel.ExchangeDeclareAsync(exchange: _exchangeName, type: ExchangeType.Topic, durable: false, autoDelete: false);
            });
        }

        private readonly Lazy<Task> _initializeTask;
    

        public async Task PublishEmailAsync(Models.Messages.UserCreateData data, CancellationToken ct = default)
        {
            await _initializeTask.Value;
            string message = JsonSerializer.Serialize(data);
            var body = Encoding.UTF8.GetBytes(message);
            await _channel.BasicPublishAsync(exchange: _exchangeName, routingKey: _routingKey, body: body);
        }
    }
}
