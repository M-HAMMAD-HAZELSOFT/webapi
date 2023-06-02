﻿using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Users>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        [HttpGet("GetById/{id}")]
        public ActionResult<Users> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        [HttpPost("Add")]
        public ActionResult<Users> AddUser(Users user)
        {
            _userService.AddUser(user);
            return Ok(new { message = "User created" });
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        [HttpPut("Update/{id}")]
        public IActionResult UpdateUser(int id, Users updatedUser)
        {
            var user = _userService.UpdateUser(id, updatedUser);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            return Ok(new { message = "User updated" });
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.DeleteUser(id);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            return Ok(new { message = "User deleted" });
        }
    }
}