using System;
using System.IO;
using System.IO.Compression;
using MeterKnife.Util.Utility;

namespace MeterKnife.Util.Zip
{
    /// <summary>对一些压缩方法的封装
    /// </summary>
    public class CompressHelper
    {
        /// <summary>判断指定的字节数组是否是被GZip压缩过的
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>
        ///   <c>true</c> if the specified bytes is compressed; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompressed(byte[] bytes)
        {
            if (UtilityCollection.IsNullOrEmpty(bytes))
                return false;
            return (bytes[0] == 31) && (bytes[1] == 139);
        }

        /// <summary>压缩指定的字节数组
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static byte[] Compress(byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                var compress = new GZipStream(ms, CompressionMode.Compress);
                compress.Write(bytes, 0, bytes.Length);
                compress.Close();
                return ms.ToArray();
            }
        }

        /// <summary>解压指定的字节数组
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static byte[] Decompress(Byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                using (var compressMs = new MemoryStream(bytes))
                {
                    var decompress = new GZipStream(compressMs, CompressionMode.Decompress);
                    decompress.CopyTo(ms);
                    decompress.Close();
                    return ms.ToArray();
                }
            }
        }
    }
}


