using System;
using System.Text;
using System.Collections.Generic;
using Xunit;

using Common;

namespace helpers.Test
{
    public class CryptoUnitTest
    {
        [Fact]
        public void ValidatePasswordTest()
        {
            string password1 = "abcde1";
            string password2 = "abcde2";

            int salt1 = 10;
            int salt2 = 11;

            var passwordHash = Crypto.GeneratePasswordHash(password1, salt1);

            {
                var result = Crypto.ValidatePassword(password1, passwordHash, salt1);
                Assert.True(result, "Simple acceptance");
            }

            {
                var passwordHash1 = Crypto.GeneratePasswordHash(password1, salt1);
                var result = Crypto.ValidatePassword(password1, passwordHash1, salt1);
                Assert.True(result, "Simple Two consecutive calls should geenrate same hash");
            }

            {
                var result = Crypto.ValidatePassword(password1, passwordHash, salt2);
                Assert.False(result, "Wrong salt");
            }

            {
                var result = Crypto.ValidatePassword(password2, passwordHash, salt1);
                Assert.False(result, "Same salt, wrong password");
            }

        }


        [Fact]
        public void GeneratePasswordHashTest()
        {
            string password1 = "aaaaa1";
            string password2 = "aaaaa2";

            int salt1 = 10;
            int salt2 = 11;

            var originalHash = Crypto.GeneratePasswordHash(password1, salt1);

            {
                var challengeHash = Crypto.GeneratePasswordHash(password1, salt1);
                var result = string.CompareOrdinal(originalHash, challengeHash) == 0;
                Assert.True(result, "Simple Two consecutive calls should geenrate same hash");
            }

            {
                var challengeHash = Crypto.GeneratePasswordHash(password1, salt2);
                var result = string.CompareOrdinal(originalHash, challengeHash) == 0;
                Assert.False(result, "Different Salt geenrate different hash");
            }

            {
                var challengeHash = Crypto.GeneratePasswordHash(password2, salt1);
                var result = string.CompareOrdinal(originalHash, challengeHash) == 0;
                Assert.False(result, "Different passwordm same salt generates different hash");
            }

            string longPassword = "qwertyuiopasdfghjklzxcvbnm 1234567890.;<>QWERTYUIOP[]ASDFGHJKLZXCVBNM,./;'1234567890-=";
            var passwordHash = Crypto.GeneratePasswordHash(longPassword, salt1);
            Assert.True(passwordHash.Length <= 50 , "Generated hash is too long");
        }
    }
}
