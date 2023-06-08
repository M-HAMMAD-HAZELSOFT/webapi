using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Services.UserService
{
    /// <summary>
    /// Implementation of the user service.
    /// </summary>
    public class UserService : IUserService
    {
        // The DataContext instance for accessing the data
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<List<Users>> GetAllUsers()
        {
            List<Users> users = await _context.Users.ToListAsync();

            return users;
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public async Task<Users> GetUserById(int id)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>A list of all users including the newly added user.</returns>
        public async Task<List<Users>> AddUser(Users newUser)
        {
            // Add the user to the database
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return _context.Users.ToList();
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        /// <returns>The updated user object, or null if the user was not found.</returns>
        public async Task<Users> UpdateUser(int id, Users updatedUser)
        {
            // Find the user to update by ID
            Users user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                // Update the user 
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user object, or null if the user was not found.</returns>
        public async Task<List<Users>> DeleteUser(int id)
        {
            // Find the user to delete by ID
            Users user = await _context.Users.FirstAsync(u => u.Id == id);

            if (user != null)
            {
                // Remove the user from the database
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return _context.Users.ToList();
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}
