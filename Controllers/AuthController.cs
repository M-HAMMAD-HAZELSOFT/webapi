using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Resources;
using webapi.BaseControllers;
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

        /// <summary>
        /// Verifies the email of registered user (new user or email changed).
        /// </summary>
        /// <param name="model">The model containing user identity, token and code.</param>
        /// <returns>Returns user login result if email is verified successfully.</returns>
        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmail model)
        {
            // Checking if the passed Model is valid
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(MessageKeys.InvalidInputParameters);
            }

            // Verifying user email
            var result = await _authService.VerifyEmail(model);

            if (!result)
                return BadRequest(MessageKeys.EmailIsNotVerified);

            // Return user login json
            return Ok(MessageKeys.EmailIstVerified);
        }
    }
}
