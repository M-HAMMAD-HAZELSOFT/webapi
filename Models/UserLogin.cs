using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    /// <summary>
    /// The User Login Model
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
