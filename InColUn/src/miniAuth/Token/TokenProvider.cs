using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

using LoggerFacade;

namespace miniAuth.Token
{
    public class TokenProvider
    {
        private readonly TokenProviderOptions options;
        private ILogger logger;
        private static SigningCredentials _signingCredentials;
        private static readonly Object s_lock = new Object();

        /// <summary>
        /// Generates new instance of TokenProvider using provided token options
        /// If signing credentials are not provided - default signing credentials will be used
        /// </summary>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        public TokenProvider(
                    TokenProviderOptions options,
                    ILogger logger)
        {
            this.logger = logger;
            this.options = options;
        }

        /// <summary>
        /// Generates Defauls SigningCredentials
        /// SigningCredentials generated from random key using HmacSha256Signature algorithm
        /// </summary>
        /// <returns>Generated SigningCredentials</returns>
        public static SigningCredentials DefaultSigningCredentials()
        {
            if (TokenProvider._signingCredentials != null) return TokenProvider._signingCredentials;

            lock(TokenProvider.s_lock)
            {
                if (TokenProvider._signingCredentials != null) return TokenProvider._signingCredentials;
                var keyForHmacSha256 = Helpers.Crypto.GetRandomBytes(64);

                var securityKey = new SymmetricSecurityKey(keyForHmacSha256);
                _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                return _signingCredentials;
            }
        }

        /// <summary>
        /// Generate Token from user information
        /// </summary>
        /// <param name="Id">User id</param>
        /// <param name="username">UserName</param>
        /// <returns>TokenResponse</returns>
        public TokenResponse GenerateUserToken(long Id, string username)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim("id", Id.ToString()),
                new Claim("name", username),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: this.options.Issuer,
                audience: this.options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(this.options.Expiration),
                signingCredentials: this.options.SigningCredentials ?? TokenProvider.DefaultSigningCredentials());

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponse
            {
                access_token = encodedJwt,
                expires_in = (ulong)this.options.Expiration.TotalSeconds
            };
        }

        public long? ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanValidateToken) return null;

            var signedCredentials = this.options.SigningCredentials ?? TokenProvider.DefaultSigningCredentials();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = this.options.Issuer,
                ValidateAudience = true,
                ValidAudience = this.options.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = signedCredentials.Key
            };


            long? result = null;
            
            try
            {
                SecurityToken validatedToken;
                var claimsPrincipal = handler.ValidateToken(accessToken, validationParameters, out validatedToken);

                foreach (var claim in claimsPrincipal.Claims)
                {
                    if (claim.Type != "id") continue;
                    long id;
                    if (long.TryParse(claim.Value, out id)) result = id;
                    break;
                }
            }
            catch(Exception e)
            {
                if (this.logger != null)
                {
                    var logEntry = new LogEntry(LoggingEventType.Error, string.Format("Token validation exception for: {0}", accessToken), e);
                    this.logger.Log(logEntry);
                }
            }

            return result;
        }

        private static void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Audience));
            }

            if (options.Expiration == TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderOptions.Expiration));
            }
        }
        public static long ToUnixEpochDate(DateTime date)
                    => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
