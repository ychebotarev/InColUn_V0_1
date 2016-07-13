using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.IdentityModel.Tokens;
using miniAuth.Token;
using System.Security.Cryptography;

namespace auth.Test
{
    [TestClass]
    public class TokesTest
    {
        [TestMethod]
        public void TokenAcceptanceTest()
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] keyForHmacSha256 = new byte[64];
            cryptoProvider.GetNonZeroBytes(keyForHmacSha256);

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

            Assert.IsNotNull(id);
            Assert.AreEqual(1, id.Value);
        }

        [TestMethod]
        public void TokenExpirationTest()
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] keyForHmacSha256 = new byte[64];
            cryptoProvider.GetNonZeroBytes(keyForHmacSha256);

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

            Assert.IsNotNull(id);
            Assert.AreEqual(1, id.Value);
        }

        public void TokenPerformanceTest()
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] keyForHmacSha256 = new byte[64];
            cryptoProvider.GetNonZeroBytes(keyForHmacSha256);

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
