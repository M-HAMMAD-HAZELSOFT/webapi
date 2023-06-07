namespace webapi.Dtos.Users
{
    public class UsersDto
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        public string? Password { get; set; }
    }
}
