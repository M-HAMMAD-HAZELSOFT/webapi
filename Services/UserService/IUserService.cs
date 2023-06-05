using webapi.Dtos.Users;
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
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        Task<ServiceResponse<GetUserDto>> GetUserById(int id);

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>A list of all users including the newly added user.</returns>
        Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user object.</param>
        /// <returns>The updated user object.</returns>
        Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updatedUser);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user object.</returns>
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
    }
}
