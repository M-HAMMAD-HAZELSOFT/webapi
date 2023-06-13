namespace webapi.Constants
{
    public class IAuthConstants
    {
        /// <summary>
        /// The jwt secret key configuration property name.
        /// The secret key will be used to encrypt jwt token.
        /// </summary>
        public const string JwtSecretKey = "Jwt:SecretKey";

        /// <summary>
        /// The email confirmation call back url configuration property name.
        /// </summary>
        public const string EmailConfirmationCallBackUrl = "EmailConfirmationCallBackUrl";
    }
}
