using webapi.Models;

namespace webapi.Services.UserService
{
    /// <summary>
    /// Implementation of the user service.
    /// </summary>
    public class UserService : IUserService
    {
        // In-memory storage for users
        private static List<Users> users = new List<Users>();

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public List<Users> GetAllUsers()
        {
            return users;
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public Users GetUserById(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>A list of all users including the newly added user.</returns>
        public List<Users> AddUser(Users user)
        {
            // Assign a unique ID to the user
            user.Id = users.Count + 1;

            // Add the user to the in-memory storage
            users.Add(user);
            return users;
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        /// <returns>The updated user object, or null if the user was not found.</returns>
        public Users UpdateUser(int id, Users updatedUser)
        {
            // Find the user to update by ID
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                // Update the user
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
            }
            return user;
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user object, or null if the user was not found.</returns>
        public Users DeleteUser(int id)
        {
            // Find the user to delete by ID
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                // Remove the user from the in-memory storage
                users.Remove(user);
            }
            return user;
        }
    }
}
