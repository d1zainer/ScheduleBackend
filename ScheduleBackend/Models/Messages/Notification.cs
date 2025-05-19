using System.ComponentModel.DataAnnotations;

namespace ScheduleBackend.Models.Messages
{
    /// <summary>
    /// Represents the data required to create a new user.
    /// </summary>
    public class UserCreateData
    {
        /// <summary>
        /// Email address of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Subject of the email.
        /// </summary>
        [Required]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Body content of the email.
        /// </summary>
        [Required]
        public string Body { get; set; } = string.Empty;

    }



    /// <summary>
    /// Represents an email message with necessary details for sending.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Recipient email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string To { get; set; } = "recipient@example.com";

        /// <summary>
        /// Subject of the email.
        /// </summary>
        [Required]
        public string Subject { get; set; } = "Default Subject";

        /// <summary>
        /// Body content of the email.
        /// </summary>
        [Required]
        public string Body { get; set; } = "Default email body.";

        /// <summary>
        /// Sender email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string From { get; set; } = "sender@example.com";

        /// <summary>
        /// Display name of the sender.
        /// </summary>
        [Required]
        public string FromName { get; set; } = "Sender Name";

        /// <summary>
        /// Password for the sender's email account.
        /// </summary>
        [Required]
        public string Password { get; set; } = "your_password";

        /// <summary>
        /// Indicates whether SSL should be used for email transmission.
        /// </summary>
        [Required]
        public bool UseSSL { get; set; } = true;

        /// <summary>
        /// SMTP server address for sending the email.
        /// </summary>
        [Required]
        public string SmtpServer { get; set; } = "smtp.example.com";

        /// <summary>
        /// Port number for SMTP communication.
        /// </summary>
        [Required]
        [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535")]
        public int Port { get; set; } = 587;
    }
}
