using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using webapi.Shared.Services.Email.Model;

namespace webapi.Shared.Services.Email
{
    /// <summary>
    /// The email sender.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="emailConfig">The email configuration.</param>
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        /// <summary>
        /// Sends the email asynchronously.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="message">The email content.</param>
        /// <returns>A task representing the asynchronous email sending proce
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMsg = new EmailMessage(new string[] { email }, subject, $"{message}");
            SendEmail(emailMsg);
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="message">The email message to be sent.</param>
        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        /// <summary>
        /// Creates a MimeMessage from the provided EmailMessage.
        /// </summary>
        /// <param name="message">The email message.</param>
        /// <returns>The constructed MimeMessage.</returns>
        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.DisplayName, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }

        /// <summary>
        /// Sends the email using SMTP.
        /// </summary>
        /// <param name="mailMessage">The MimeMessage to be sent.</param>
        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
