using System;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using miniAuth.Token;
using System.Security.Cryptography;
using Xunit;

namespace auth.Test
{
    public class TokenTest
    {
        [Fact]
        public void TokenAcceptanceTest()
        {
            byte[] keyForHmacSha256 = new byte[64];

            var randomGen = RandomNumberGenerator.Create();
            randomGen.GetBytes(keyForHmacSha256);

            var securityKey = new SymmetricSecurityKey(keyForHmacSha256);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var options = new TokenProviderOptions
            {
                Issuer = "InColUn",
                Audience = "All",
                SigningCredentials = signingCredentials,
                Expiration = TimeSpan.FromMilliseconds(1000)
            };

            var provider = new TokenProvider(options, null);

            var token = provider.GenerateUserToken(1, "John Dwo");

            var id = provider.ValidateToken(token.access_token);

            Assert.NotNull(id);
            Assert.Equal(1, id.Value);
        }

        [Fact]
        public void TokenExpirationTest()
        {
            byte[] keyForHmacSha256 = new byte[64];

            var randomGen = RandomNumberGenerator.Create();
            randomGen.GetBytes(keyForHmacSha256);


            var securityKey = new SymmetricSecurityKey(keyForHmacSha256);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var options = new TokenProviderOptions
            {
                Issuer = "InColUn",
                Audience = "All",
                SigningCredentials = signingCredentials,
                Expiration = TimeSpan.FromMilliseconds(1000)
            };

            var provider = new TokenProvider(options, null);

            var token = provider.GenerateUserToken(1, "John Dwo");

            var id = provider.ValidateToken(token.access_token);

            Assert.NotNull(id);
            Assert.Equal(1, id.Value);
        }

        public void TokenPerformanceTest()
        {
            byte[] keyForHmacSha256 = new byte[64];
            var randomGen = RandomNumberGenerator.Create();
            randomGen.GetBytes(keyForHmacSha256);

            var securityKey = new SymmetricSecurityKey(keyForHmacSha256);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var options = new TokenProviderOptions
            {
                Issuer = "InColUn",
                Audience = "All",
                SigningCredentials = signingCredentials
            };

            var provider = new TokenProvider(options, null);

            for (int i = 0; i < 128 * 2; i++)
            {
                var token = provider.GenerateUserToken(1, "John Dwo");
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            int limit = 128 * 1024;
            for (int i = 0; i < limit; i++)
            {
                var token = provider.GenerateUserToken(i, "John Dwo");
            }
            stopwatch.Stop();

            var output = string.Format("{0} tokens/sec", 1000.0 * limit / stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine(output);
        }
    }
}