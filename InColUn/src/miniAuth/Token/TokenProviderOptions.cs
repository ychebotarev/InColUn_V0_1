using System;
using Microsoft.IdentityModel.Tokens;

namespace miniAuth.Token
{
    public class TokenProviderOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(60);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
