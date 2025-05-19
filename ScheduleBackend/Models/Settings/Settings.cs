namespace ScheduleBackend.Models.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int ExpiryMinutes { get; set; }
    }

    public class RabbitMqSettings
    {
        /// <summary>
        /// Hostname of the RabbitMQ server.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Exchange name used in RabbitMQ.
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// Queue name for message processing.
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// Routing key for message delivery.
        /// </summary>
        public string RoutingKey { get; set; }
    }




    /// <summary>
    /// Represents the settings required to configure an email server.
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Sender's email address.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// SMTP server port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Password for the email account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Indicates whether SSL should be used.
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// SMTP server address.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Username for authentication.
        /// </summary>
        public string UserName { get; set; }
    }

}
