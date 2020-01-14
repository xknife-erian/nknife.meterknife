using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace NKnife.Utility
{
    public class UtilityByte
    {
        public static int MakeInt(byte[] bs)
        {
            byte b3 = bs[0];
            byte b2 = bs[1];
            byte b1 = bs[2];
            byte b0 = bs[3];
            return (int)((((b3 & 0xff) << 24) | ((b2 & 0xff) << 16) | ((b1 & 0xff) << 8) | ((b0 & 0xff) << 0)));
        }

        /// <summary>
        /// 比较字节数组
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        public static bool Compare(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length)
                return false;
            return b1.Where((t, i) => t.Equals(b2[i])).Any();
        }

        /// <summary>
        /// 用memcmp比较字节数组
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        /// <returns>如果两个数组相同，返回0；如果数组1小于数组2，返回小于0的值；如果数组1大于数组2，返回大于0的值。</returns>
        public static int MemoryCompare(byte[] b1, byte[] b2)
        {
            IntPtr retval = memcmp(b1, b2, new IntPtr(b1.Length));
            return retval.ToInt32();
        }

        /// <summary>
        /// memcmp API
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        /// <param name="count"></param>
        /// <returns>如果两个数组相同，返回0；如果数组1小于数组2，返回小于0的值；如果数组1大于数组2，返回大于0的值。</returns>
        [DllImport("msvcrt.dll")]
        private static extern IntPtr memcmp(byte[] b1, byte[] b2, IntPtr count);
    }
}
