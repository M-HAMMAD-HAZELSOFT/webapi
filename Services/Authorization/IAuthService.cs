using webapi.Models;

namespace webapi.Services.Authorization
{
    /// <summary>
    /// Interface for managing user.
    /// </summary>
    public interface IAuthService
    {
        Task<string> Login(UserLogin user);

        Task<bool> Signup(UserSignup user);

        /// <summary>
        /// Verifies the email.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task<bool> VerifyEmail(VerifyEmail model);
    }
}
