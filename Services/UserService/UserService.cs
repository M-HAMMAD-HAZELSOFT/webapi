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
        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new ServiceResponse<List<GetUserDto>>();
            serviceResponse.Items = (users.Select(c => _mapper.Map<GetUserDto>(c))).ToList();
            return serviceResponse;
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            ServiceResponse<GetUserDto> serviceResponse = new ServiceResponse<GetUserDto>();
            serviceResponse.Items = _mapper.Map<GetUserDto>(users.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>A list of all users including the newly added user.</returns>
        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new ServiceResponse<List<GetUserDto>>();

            Users user = _mapper.Map<Users>(newUser);

            // Assign a unique ID to the user
            // user.Id = users.Max(u => u.Id) + 1;
            user.Id = users.Count + 1;

            // Add the user to the in-memory storage
            users.Add(user);

            serviceResponse.Items = (users.Select(u => _mapper.Map<GetUserDto>(u))).ToList();

            return serviceResponse;
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        /// <returns>The updated user object, or null if the user was not found.</returns>
        public async Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserDto> serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                // Find the user to update by ID
                Users user = users.First(u => u.Id == id);

                // Update the user 
                user.Name = updatedUser.Name; 
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
                serviceResponse.Items = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user object, or null if the user was not found.</returns>
        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new ServiceResponse<List<GetUserDto>>();

            try
            {
                // Find the user to delete by ID
                Users user = users.First(u => u.Id == id);

                // Remove the user from the in-memory storage
                users.Remove(user);

                serviceResponse.Items = (users.Select(c => _mapper.Map<GetUserDto>(c))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
