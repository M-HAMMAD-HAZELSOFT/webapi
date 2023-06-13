using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Models;
using webapi.Constants;
using webapi.Services.Email;

namespace webapi.Services.Authorization
{
    /// <summary>
    /// Implementation of the auth service.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthEmailService _authEmailService;

        public AuthService(IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IAuthEmailService authEmailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _authEmailService = authEmailService;
        }

        public async Task<string> Login(UserLogin user)
        {
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, user.Password, false, false);

            if (result.Succeeded)
            {
                return CreateToken(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Signup(UserSignup user)
        {
            var userExsists = await _userManager.FindByNameAsync(user.UserName);

            if (userExsists == null)
            {
                var newUser = new IdentityUser
                {
                    Email = user.Email,
                    UserName = user.UserName
                };

                // Creates a new User
                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    //Send Confirmation email
                    await _authEmailService.SendEmailConfirmation(newUser.Id, token, newUser.Email, user.Password);

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verifies the email.
        /// </summary>
        /// <param name="dto">The verify email dto.</param>
        /// <returns>A Task of UserRegistrationResult.</returns>
        public async Task<bool> VerifyEmail(VerifyEmail model)
        {
            try
            {
                var authUser = await _userManager.FindByIdAsync(model.UserId);
                if (authUser == null) return false;

                // Verify email
                string email = Encoding.Default.GetString(Convert.FromBase64String(model.Email));
                if (string.Compare(email, authUser.Email, true) != 0) return false;

                // Verify token
                string token = Encoding.Default.GetString(Convert.FromBase64String(model.Token));

                if (model.Flag == 0)
                {
                    var result = await _userManager.ConfirmEmailAsync(authUser, token);
                    if (result.Succeeded)
                        return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        private string CreateToken(UserLogin user)
        {
            // JWT private key from the config
            var securityKey = Encoding.UTF8.GetBytes(_configuration.GetSection(IAuthConstants.JwtSecretKey).Value);

            // creating the claims list for the JWT token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(securityKey);

            // creating the credentials from the key
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // object get the info used to create final token
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            // Writing the Token to a String
            return tokenHandler.WriteToken(token);
        }
    }
}
