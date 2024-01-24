using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    /// <summary>
    /// The verify email Model.
    /// The properties in following Model are generated and appended as a query string parameter link to the URL,
    /// which is sent to user's email address, when reqeusted.
    /// Email verification is sent when new user is created or its email address is changed.
    /// Password reset link is sent when user forgots its password and wants to reset it.
    /// </summary>
    public class VerifyEmail
    {
        /// <summary>
        /// Gets or sets the flag.
        /// Flag = 0 means model contains code / token for email verification.
        /// Flag = 1 means model contains code / token for password reset.
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// Gets or sets the asp.net user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the token generated for verification.
        /// In case of email verification, email confirmation token is generated using microsoft identity framework. 
        /// In case of password reset, password reset token is generated using microsoft identity framework.
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// This is required to check if email address is not changed in between subsequent requests.
        /// If email address is changed after link is generated, verification will fail.
        /// </summary>
        public string Email { get; set; }
    }
}
