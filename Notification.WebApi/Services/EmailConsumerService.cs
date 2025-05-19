using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using MimeKit;
using MimeKit.Text;
using Notification.WebApi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notification.WebApi.Services;

public class EmailConsumerService : BackgroundService
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IChannel _channel;

    private readonly RabbitMqSettings _rabbitMQSettings;
    private readonly EmailSettings _emailSettings;
    private readonly EmailSendService _emailSendService;


    public EmailConsumerService(RabbitMqSettings rabbitSettings, EmailSendService emailSendService,
        EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
        _emailSendService = emailSendService;
        _rabbitMQSettings = rabbitSettings;
        _factory = new ConnectionFactory { HostName = _rabbitMQSettings.HostName };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connection = await _factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        var exchangeName = _rabbitMQSettings.ExchangeName;
        var queueName = _rabbitMQSettings.QueueName;
        var routingKey = _rabbitMQSettings.RoutingKey;

        await _channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Topic, false, false);
        await _channel.QueueDeclareAsync(queueName, false, false, false);
        await _channel.QueueBindAsync(queueName, exchangeName, routingKey);

        Console.WriteLine(" [*] Waiting for messages...");
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += HandleMessageAsync;

        await _channel.BasicConsumeAsync(queueName, true, consumer);
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }


    public async Task HandleMessageAsync(object model, BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        var userData = JsonSerializer.Deserialize<UserCreateData>(message);

        var context = new ValidationContext(userData);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(userData, context, results, true))
            return;
        try
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.UserName, _emailSettings.From));
            emailMessage.To.Add(new MailboxAddress("", userData.Email));
            emailMessage.Subject = userData.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = userData.Body
            };
            await _emailSendService.SendEmailAsync(emailMessage);
            Console.WriteLine($" [x] Received user: {userData}");
        }
        catch (Exception ex) { Console.WriteLine($" [!] Error deserializing message: {ex.Message}"); return; } 
        await Task.CompletedTask;
    }


    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        await _channel.CloseAsync();
        await _connection.CloseAsync();
        await base.StopAsync(stoppingToken);
    }
}