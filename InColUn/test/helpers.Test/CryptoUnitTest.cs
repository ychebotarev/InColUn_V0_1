using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace helpers.Test
{
    /// <summary>
    /// Summary description for CryptoUnitTest
    /// </summary>
    [TestClass]
    public class CryptoUnitTest
    {
        [TestMethod]
        public void ValidatePasswordTest()
        {
            string password1 = "abcde1";
            string password2 = "abcde2";

            int salt1 = 10;
            int salt2 = 11;

            var passwordHash = Helpers.Crypto.GeneratePasswordHash(password1, salt1);

            {
                var result = Helpers.Crypto.ValidatePassword(password1, passwordHash, salt1);
                result.Should().Be(true, "Simple acceptance");
            }

            {
                var passwordHash1 = Helpers.Crypto.GeneratePasswordHash(password1, salt1);
                var result = Helpers.Crypto.ValidatePassword(password1, passwordHash1, salt1);
                result.Should().Be(true, "Simple Two consecutive calls should geenrate same hash");
            }

            {
                var result = Helpers.Crypto.ValidatePassword(password1, passwordHash, salt2);
                result.Should().Be(false, "Wrong salt");
            }

            {
                var result = Helpers.Crypto.ValidatePassword(password2, passwordHash, salt1);
                result.Should().Be(false, "Same salt, wrong password");
            }

        }


        [TestMethod]
        public void GeneratePasswordHashTest()
        {
            string password1 = "aaaaa1";
            string password2 = "aaaaa2";

            int salt1 = 10;
            int salt2 = 11;

            var originalHash = Helpers.Crypto.GeneratePasswordHash(password1, salt1);

            {
                var challengeHash = Helpers.Crypto.GeneratePasswordHash(password1, salt1);
                var result = string.CompareOrdinal(originalHash, challengeHash) == 0;
                result.Should().Be(true, "Simple Two consecutive calls should geenrate same hash");
            }

            {
                var challengeHash = Helpers.Crypto.GeneratePasswordHash(password1, salt2);
                var result = string.CompareOrdinal(originalHash, challengeHash) == 0;
                result.Should().Be(false, "Different Salt geenrate different hash");
            }

            {
                var challengeHash = Helpers.Crypto.GeneratePasswordHash(password2, salt1);
                var result = string.CompareOrdinal(originalHash, challengeHash) == 0;
                result.Should().Be(false, "Different passwordm same salt generates different hash");
            }

            string longPassword = "qwertyuiopasdfghjklzxcvbnm 1234567890.;<>QWERTYUIOP[]ASDFGHJKLZXCVBNM,./;'1234567890-=";
            var passwordHash = Helpers.Crypto.GeneratePasswordHash(longPassword, salt1);
            passwordHash.Length.Should().BeLessThan(51, "Generated hash is too long");
        }
    }
}
