using Microsoft.AspNetCore.Mvc;
using webapi.Dtos.Users;
using webapi.Models;
using webapi.Services.UserService;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            ServiceResponse<GetUserDto> response = await _userService.GetUserById(id);
            if (response.Items == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        [HttpPost("Add")]
        public async Task<ActionResult> AddUser(AddUserDto newUser)
        {
             return Ok(await _userService.AddUser(newUser));
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserDto> response = await _userService.UpdateUser(id, updatedUser);
            if(response.Items == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserDto>> response = await _userService.DeleteUser(id);
            if (response.Items == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
