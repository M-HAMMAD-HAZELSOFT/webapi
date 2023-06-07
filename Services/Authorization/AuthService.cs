using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Data;
using webapi.Models;

namespace webapi.Services.Authorization
{
    /// <summary>
    /// Implementation of the auth service.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <param name="password">The password to add.</param>
        /// <returns>The newly added user id.</returns>
        public async Task<int> AddUser(Users user, string password)
        {
            if (await AlreadyExists(user.UserName))
            {
                throw new Exception("User already exist.");
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        /// <summary>
        /// Login a user.
        /// </summary>
        /// <param name="username">The username to match.</param>
        /// <param name="password">The password to match.</param>
        /// <returns>The token.</returns>
        public async Task<string> Login(string username, string password)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(
                x => x.UserName.ToLower().Equals(username.ToLower()));

            if (user == null)
            {
                throw new Exception("User not found.");
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Password is wrong.");
            }

            return CreateToken(user);
        }

        public async Task<bool> AlreadyExists(string username)
        {
            // Check if any user with the same username already exists in the database
            var result = await _context.Users.AnyAsync(x => string.IsNullOrEmpty(username)
            || x.UserName.ToLower() == username.ToLower());

            // Return the result directly (true if user already exists, false otherwise)
            return result;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash,
            out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // Generate a random salt and assign it to the passwordSalt
                passwordSalt = hmac.Key;
                // Compute the hash of the provided password using the salt
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // Compute the hash of the provided password
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    // Compare each byte of the computed hash with the stored password hash
                    if (computedHash[i] != passwordHash[i])
                    {
                        // If any byte doesn't match, return false (passwords do not match)
                        return false;
                    }
                }
                // If all bytes match, return true (passwords match)
                return true;
            }
        }

        private string CreateToken(Users user)
        {
            // creating the claims list for the JWT token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            // JWT private key from the config
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            // creating the credentials from the key
            SigningCredentials creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha512Signature);

            // object get the info used to create final token
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            // Writing the Token to a String
            return tokenHandler.WriteToken(token);
        }
    }
}
