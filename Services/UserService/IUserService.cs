using webapi.Models;

namespace webapi.Services.UserService
{
    /// <summary>
    /// Interface for managing user data.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        Task<List<Users>> GetAllUsers();

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        Task<Users> GetUserById(int id);

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>The newly added user.</returns>
        Task<Users> AddUser(Users newUser);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="updatedUser">The updated user object.</param>
        /// <returns>The updated user object.</returns>
        Task<Users> UpdateUser(Users updatedUser);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The boolean flag.</returns>
        Task<bool> DeleteUser(int id);
    }
}
