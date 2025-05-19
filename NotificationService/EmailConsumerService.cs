using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ScheduleBackend.Models.Messages;
using ScheduleBackend.Models.Settings;

namespace Notification.WebApi.Services;

public class EmailConsumerService : BackgroundService
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IChannel _channel;

    private readonly RabbitMqSettings _rabbit;
    private readonly EmailSettings _email;
    private readonly EmailSendService _sender;


    public EmailConsumerService(
        IOptions<RabbitMqSettings> rabbitOpt,
        IOptions<EmailSettings> emailOpt,
        EmailSendService sender)
    {
        _rabbit = rabbitOpt.Value;
        _email = emailOpt.Value;
        _sender = sender;

        _factory = new ConnectionFactory
        {
            HostName = _rabbit.HostName
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connection = await _factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        var exchangeName = _rabbit.ExchangeName;
        var queueName = _rabbit.QueueName;
        var routingKey = _rabbit.RoutingKey;

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
            emailMessage.From.Add(new MailboxAddress(_email.UserName, _email.From));
            emailMessage.To.Add(new MailboxAddress("", userData.Email));
            emailMessage.Subject = userData.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = userData.Body
            };
            await _sender.SendEmailAsync(emailMessage);
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