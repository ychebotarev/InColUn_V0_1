using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Helpers
{
    public class Crypto
    {
        private static Random random = new Random();
        public static void Init(int? seed)
        {
            Crypto.random = seed.HasValue? new Random(seed.Value): new Random();
        }

        public static Tuple<string, long> GeneratePasswordHashSalt(string password)
        {
            int salt = Crypto.random.Next();

            return new Tuple<string, long>(GeneratePasswordHash(password, salt), salt);
        }

        public static string GeneratePasswordHash(string password, long salt)
        {
            var saltBytes = BitConverter.GetBytes(salt);
            var password_hash = new Rfc2898DeriveBytes(password, saltBytes, 100).GetBytes(25);

            return System.ByteArrayToHex(password_hash);
        }

        public static string GeneratePasswordHashSHA256(string password, long salt)
        {
            var saltBytes = BitConverter.GetBytes(salt);
            byte[] passwordByted = Encoding.UTF8.GetBytes(password);
            var bp = System.ConcatByteArrays(passwordByted, saltBytes);
            var hashAlgorithm = SHA256.Create();
            var password_hash = hashAlgorithm.ComputeHash(bp);

            if (password_hash.Length > 25)
            {
                Array.Resize(ref password_hash, 25);
            }
            return System.ByteArrayToHex(password_hash);
        }

        public static bool ValidatePassword(string password, string passwordHash, long salt)
        {
            var generatedHash = GeneratePasswordHash(password, salt);
            return string.CompareOrdinal(passwordHash, generatedHash) == 0;
        }

        public static byte[] GetRandomBytes(int cnt)
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[cnt];
            rng.GetBytes(bytes);

            //var cryptoProvider = new RNGCryptoServiceProvider();
            //byte[] bytes = new byte[cnt];
            //cryptoProvider.GetNonZeroBytes(bytes);

            return bytes;
        }
    }
}
