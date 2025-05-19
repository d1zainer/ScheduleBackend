using MimeKit;
using System.ComponentModel.DataAnnotations;
using Notification.WebApi.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Notification.WebApi.Services
{
    public class EmailSendService(EmailSettings emailSettings)
    {

        public async  Task<(bool, string)> SendEmailAsync(EmailMessage message)
        {
            var context = new ValidationContext(message);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(message, context, results, true))
            {
                string errors = string.Join(", ", results.Select(r => r.ErrorMessage));
                return (false, errors);
            }

            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(message.FromName, message.From));
            emailMessage.To.Add(new MailboxAddress("", message.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Body };
            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(message.SmtpServer, message.Port, message.UseSSL);
                await smtp.AuthenticateAsync(message.From, message.Password);
                await smtp.SendAsync(emailMessage);
                await smtp.DisconnectAsync(true);
                Console.WriteLine("Email sent successfully");
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return (false, ex.Message);
            }
        }


        public async Task SendEmailAsync(MimeMessage message)
        {
            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(emailSettings.SmtpServer, emailSettings.Port, emailSettings.UseSSL);
                await smtp.AuthenticateAsync(emailSettings.From, emailSettings.Password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
                Console.WriteLine("Email sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

    }
}
