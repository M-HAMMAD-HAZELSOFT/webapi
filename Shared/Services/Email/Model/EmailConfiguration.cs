namespace webapi.Shared.Services.Email.Model
{
    /// <summary>
    /// The email configurations
    /// </summary>
    public class EmailConfiguration
    {
        /// <summary>
        /// Get or set From where the email is sent.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Get or set the SmtpServer.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Get or set the Port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Get or set the username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get or set the Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Get or set the Display name of Email 
        /// </summary>
        public string DisplayName { get; set; }

    }
}
