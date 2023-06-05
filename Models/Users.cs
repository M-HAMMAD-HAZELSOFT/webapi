using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    /// <summary>
    /// The Users Model
    /// </summary>
    public class Users
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        [Required]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        [Required]
        public string? Password { get; set; }
    }
}
