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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        public byte[] PasswordSalt { get; set; }

    }
}
