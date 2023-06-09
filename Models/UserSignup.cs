using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    /// <summary>
    /// The User Signup Model
    /// </summary>
    public class UserSignup
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
