namespace webapi.Dtos.Contact
{
    /// <summary>
    /// The Contact Dto
    /// </summary>
    public class ContactDto
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string? Address { get; set; }
    }
}
