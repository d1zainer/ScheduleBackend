using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.WebApi.Services;
using ScheduleBackend.Models.Settings;

namespace NotificationService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // 1) Настройки: appsettings.json → user-secrets → env-vars
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>(optional: true)
                .AddEnvironmentVariables();

            // ── strongly-typed options ──────────────────────────────────────────────
            builder.Services.Configure<RabbitMqSettings>(
                builder.Configuration.GetSection("RabbitMQ"));
            builder.Services.Configure<EmailSettings>(
                builder.Configuration.GetSection("EmailService"));

            // ── инфраструктура SMTP / Rabbit (пример SMTP-сервиса опущен) ───────────
            builder.Services.AddSingleton<EmailSendService>();

            // ── фоновый consumer ────────────────────────────────────────────────────
            builder.Services.AddHostedService<EmailConsumerService>();

            await builder.Build().RunAsync();
        }
    }
}
