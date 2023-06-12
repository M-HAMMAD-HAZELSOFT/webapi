using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.Resources;
using webapi.Repositories;

namespace webapi.Services.UserService
{
    /// <summary>
    /// Implementation of the user service.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Users> _genericRepository;

        public UserService(DataContext context)
        {
            _genericRepository = new GenericRepository<Users>(context);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<List<Users>> GetAllUsers()
        {
            List<Users> users = (List<Users>)await _genericRepository.GetAll();

            return users;
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public async Task<Users> GetUserById(int id)
        {
            Users user = await _genericRepository.GetById(x => x.Id == id).FirstOrDefaultAsync();

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception(MessageKeys.UserNotFound);
            }
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>The newly added user.</returns>
        public async Task<Users> AddUser(Users newUser)
        {
            // Add the user to the database
            _genericRepository.Insert(newUser);

            return newUser;
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="updatedUser">The updated user details.</param>
        /// <returns>The updated user object, or null if the user was not found.</returns>
        public async Task<Users> UpdateUser(Users updatedUser)
        {
            _genericRepository.Update(updatedUser);

            // Find the user to update by ID
            Users user = await _genericRepository.GetById(x => x.Id == updatedUser.Id).FirstOrDefaultAsync();

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception(MessageKeys.UserNotFound);
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The boolean flag.</returns>
        public async Task<bool> DeleteUser(int id)
        {
            // Find the user to delete by ID
            var user = await _genericRepository.GetById(x => x.Id == id).FirstOrDefaultAsync();

            if (user != null)
            {
                // Remove the user from the database
                return _genericRepository.Delete(user);
            }
            else
            {
                throw new Exception(MessageKeys.UserNotFound);
            }
        }
    }
}
