using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Models;
using webapi.Constants;

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

        public AuthService(IConfiguration configuration,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
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
            var userExsists = _userManager.FindByNameAsync(user.UserName);

            if (!userExsists.IsCompleted)
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
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return true;
                }
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
