using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace helpers.Test
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void TestByteArrayCompare()
        {
            byte[] b1 = new byte[5];
            byte[] b2 = new byte[5];

            var random = new Random();
            for (int i = 0; i < b1.Length; ++i)
            {
                b1[i] = b2[i] = (byte)(random.Next() % 256);
            }

            var result = Helpers.System.ByteArrayCompare(b1, b2);
            result.Should().Be(true, "Same byte arrays");

            b1[0] = (byte)(random.Next() % 256);
            b2[0] = (byte)(random.Next() % 256);

            result = Helpers.System.ByteArrayCompare(b1, b2);
            result.Should().Be(false, "First byte is different");

            random.NextBytes(b1);
            random.NextBytes(b2);

            result = Helpers.System.ByteArrayCompare(b1, b2);
            result.Should().Be(false, "Completely different byte arrays");
        }
    }
}
