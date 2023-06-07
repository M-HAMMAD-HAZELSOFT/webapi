using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Dtos.Users;
using webapi.BaseControllers;
using webapi.Services.Authorization;

namespace webapi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        [HttpPost("Signup")]
        public async Task<ActionResult> AddUser(UsersDto newUser)
        {
            try
            {
                return Ok(new
                {
                    Items = await _authService.AddUser(
                        new Users { UserName = newUser.UserName }, newUser.Password)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// login a user.
        /// </summary>
        /// <param name="user">The user to login.</param>
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UsersDto user)
        {
            try
            {
                return Ok(new { Items = await _authService.Login(user.UserName, user.Password) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
