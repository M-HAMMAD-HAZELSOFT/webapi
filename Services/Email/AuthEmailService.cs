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
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="token">Email confirmation token.</param>
        /// <param name="email">The user email address.</param>
        /// <param name="password">The user password.</param>
        /// <returns>A Task.</returns>
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
            token = Convert.ToBase64String(Encoding.Default.GetBytes(token));
            email = Convert.ToBase64String(Encoding.Default.GetBytes(email));

            return $"{_configuration[IAuthConstants.EmailConfirmationCallBackUrl]}?flag={flag}&userId={userId}&token={token}&email={email}";
        }
    }
}
