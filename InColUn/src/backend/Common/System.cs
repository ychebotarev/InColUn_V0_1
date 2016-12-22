using System;
using System.Runtime.InteropServices;

namespace Common
{
    public class System
    {
        private static readonly uint[] _lookup32 = CreateLookup32();

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        public static unsafe bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            // Validate buffers are the same length.
            // This also ensures that the count does not exceed the length of either buffer.  
            fixed (byte* p1 = b1)
            {
                fixed (byte* p2 = b2)
                {
                    IntPtr ptr1 = (IntPtr)p1;
                    IntPtr ptr2 = (IntPtr)p2;
                    // do you stuff here
                    return b1.Length == b2.Length && memcmp(ptr1, ptr2, b1.Length) == 0;
                }
            }
        }

        public static byte[] ConcatByteArrays(Array arrayA, Array arrayB)
        {
            byte[] outputBytes = new byte[arrayA.Length + arrayB.Length];
            Buffer.BlockCopy(arrayA, 0, outputBytes, 0, arrayA.Length);
            Buffer.BlockCopy(arrayB, 0, outputBytes, arrayA.Length, arrayB.Length);
            return outputBytes;
        }

        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }


        //can be made twice as fast with unsafe
        //http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa
        //http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa/24343727#24343727
        //https://github.com/patridge/PerformanceStubs/blob/master/PerformanceStubs/Tests/ConvertByteArrayToHexString/Test.cs

        public static string ByteArrayToHex(byte[] bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }
    }
}