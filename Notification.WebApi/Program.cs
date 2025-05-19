
using Notification.WebApi.Models;
using Notification.WebApi.Services;

namespace Notification.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var emailSettings = builder.Configuration
                .GetSection("EmailService")
                .Get<EmailSettings>() ?? throw new InvalidOperationException("Email settings not found");

            var rabbitSettings = builder.Configuration
                .GetSection("RabbitMQ")
                .Get<RabbitMqSettings>() ?? throw new InvalidOperationException("RabbitMQ settings not found");

            builder.Services.AddSingleton(emailSettings);
            builder.Services.AddSingleton(rabbitSettings);

            builder.Services.AddSingleton<EmailSendService>();
            builder.Services.AddHostedService<EmailConsumerService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
