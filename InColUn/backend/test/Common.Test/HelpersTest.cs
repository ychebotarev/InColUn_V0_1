using System;
using Xunit;

namespace helpers.Test
{
    public class HelpersTest
    {
        [Fact]
        public void TestByteArrayCompare()
        {
            byte[] b1 = new byte[5];
            byte[] b2 = new byte[5];

            var random = new Random();
            for (int i = 0; i < b1.Length; ++i)
            {
                b1[i] = b2[i] = (byte)(random.Next() % 256);
            }

            var result = Common.System.ByteArrayCompare(b1, b2);
            Assert.True(result);

            b1[0] = (byte)(random.Next() % 256);
            b2[0] = (byte)(random.Next() % 256);

            result = Common.System.ByteArrayCompare(b1, b2);
            Assert.False(result);


            random.NextBytes(b1);
            random.NextBytes(b2);

            result = Common.System.ByteArrayCompare(b1, b2);
            Assert.False(result);
        }
    }
}
