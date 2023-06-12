using Microsoft.AspNetCore.Mvc;
using webapi.BaseControllers;
using webapi.Models;
using webapi.Resources;
using webapi.Services.Authorization;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(UserSignup user)
        {
            var result = await _authService.Signup(user);

            if (ModelState.IsValid && result)
            {
                return Ok(MessageKeys.UserRegistered);
            }
            else
            {
                return BadRequest(MessageKeys.UserNotRegisteredOrAlreadyExists);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            var token = await _authService.Login(user);
            if (token != null)
            {
                return Ok(new { token = token }, MessageKeys.UserLoginSuccessfully);
            }
            else
            {
                return BadRequest(MessageKeys.InvalidUserNameOrPassword);
            }
        }
    }
}
