using AutoMapper;
using webapi.Dtos.Users;
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

        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<List<UsersDto>> GetAllUsers()
        {
            return (users.Select(c => _mapper.Map<UsersDto>(c))).ToList();
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public async Task<UsersDto> GetUserById(string id)
        {
            UsersDto result = _mapper.Map<UsersDto>(users.FirstOrDefault(c => c.Id == id));
            if (result != null)
            {
                return result;
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
        public async Task<List<UsersDto>> AddUser(UsersDto newUser)
        {
            Users user = _mapper.Map<Users>(newUser);

            user.Id = Guid.NewGuid().ToString();

            // Add the user to the in-memory storage
            users.Add(user);

            return (users.Select(u => _mapper.Map<UsersDto>(u))).ToList();
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        /// <returns>The updated user object, or null if the user was not found.</returns>
        public async Task<UsersDto> UpdateUser(string id, UsersDto updatedUser)
        {
            // Find the user to update by ID
            Users user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                // Update the user 
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
                return _mapper.Map<UsersDto>(user);
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
        public async Task<List<UsersDto>> DeleteUser(string id)
        {
            // Find the user to delete by ID
            Users user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                // Remove the user from the in-memory storage
                users.Remove(user);

                return (users.Select(c => _mapper.Map<UsersDto>(c))).ToList();
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}
