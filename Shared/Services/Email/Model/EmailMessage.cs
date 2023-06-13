using MimeKit;

namespace webapi.Shared.Services.Email.Model
{
    /// <summary>
    /// The Email message class
    /// </summary>
    public class EmailMessage
    {

        /// <summary>
        /// Address where we want to send email
        /// </summary>
        public List<MailboxAddress> To { get; set; }

        /// <summary>
        /// Get or set the Subject of email
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Get or set the Content of email
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// To send email message
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        public EmailMessage(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("WebApi", x)));
            Subject = subject;
            Content = content;
        }
    }
}
