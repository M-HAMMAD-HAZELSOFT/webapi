using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    /// <summary>
    /// The Contact Model
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        [Required]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Required]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [Required]
        public string? Address { get; set; }
    }
}
