using Microsoft.AspNetCore.Mvc;
using webapi.Dtos.Users;
using webapi.Services.UserService;
using webapi.BaseControllers;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
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
            return Ok(new { Items = await _userService.GetAllUsers() });
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            try
            {
                return Ok(new { Items = await _userService.GetUserById(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        [HttpPost("Add")]
        public async Task<ActionResult> AddUser(UsersDto newUser)
        {
            return Ok(new { Items = await _userService.AddUser(newUser) });
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, UsersDto updatedUser)
        {
            try
            {
                return Ok(new { Items = await _userService.UpdateUser(id, updatedUser) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                return Ok(new { Items = await _userService.DeleteUser(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
