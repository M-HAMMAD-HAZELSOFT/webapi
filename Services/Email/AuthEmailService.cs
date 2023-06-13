using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text;
using webapi.Constants;

namespace webapi.Services.Email
{
    /// <summary>
    /// The auth service implementation
    /// </summary>
    public class AuthEmailService : IAuthEmailService
    {
        /// <summary>
        /// The email confirmation link id.
        /// </summary>
        const int EmailConfirmationLinkId = 0;

        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public AuthEmailService(IEmailSender emailSender, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _configuration = configuration;
        }

        /// <summary>
        /// Sends the email confirmation.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <param name="email">The user email address.</param>
        /// <param name="password">The user password.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendEmailConfirmation(string userId, string token, string email, string password)
        {
            try
            {
                var callbackUrl = GetCallbackUrl(EmailConfirmationLinkId, userId, token, email);

                await _emailSender.SendEmailAsync(email, "Confirm your account",
                   "Please confirm your account by clicking on the following link: <a href=\""
                                                   + callbackUrl
                                                   + "\">Confirm Email</a> <br/>");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetCallbackUrl(int flag, string userId, string token, string email)
        {
            // Encode the token and email as Base64 strings
            token = Convert.ToBase64String(Encoding.Default.GetBytes(token));
            email = Convert.ToBase64String(Encoding.Default.GetBytes(email));

            // Build the callback URL with the encoded parameters
            return $"{_configuration[IAuthConstants.EmailConfirmationCallBackUrl]}?flag={flag}&userId={userId}&token={token}&email={email}";
        }
    }
}
