using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NKnife.Utility;

namespace System
{
    public static class BytesExtension
    {
        /// <summary>判断是否为null,empty,或由指定的数据组成
        /// </summary>
        public static bool IsNullOrEmptyOrConsistBy(this byte[] bytes, byte element = 0x00)
        {
            if (bytes == null || bytes.Length <= 0)
                return true;
            if (bytes.Any(b => b != element))
                return false;
            return true;
        }

        /// <summary>
        /// 字节数组转换成字符串，
        /// 针对java程序socket发给C#客户端时，
        /// 有时候字节流头三个字节bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191
        /// 做了处理
        /// </summary>
        public static string ToString(this byte[] bytes, Encoding encoding)
        {
            if (!Equals(encoding, Encoding.UTF8)) return encoding.GetString(bytes);
            if (bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191)
            {
                return encoding.GetString(bytes, 3, bytes.Length - 3);
            }
            return encoding.GetString(bytes);
        }

        /// <summary>转换为十六进制字符串
        /// </summary>
        public static string ToHexString(this byte b)
        {
            return b.ToString("X2");
        }
        /// <summary>转换为十六进制字符串
        /// </summary>
        public static string ToHexString(this IEnumerable<byte> bytes, char spliter = ' ')
        {
            if (bytes == null)
                return string.Empty;
            var sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2")).Append(spliter);
            return sb.ToString().TrimEnd(spliter);
        }

        /// <summary>转换为Base64字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        /// <summary>转换为基础数据类型
        /// </summary>
        public static int ToInt(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }
        /// <summary>转换为基础数据类型
        /// </summary>
        public static long ToInt64(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }

        /// <summary>转换为基础数据类型
        /// </summary>
        public static bool ToBoolean(this byte[] value, int startIndex)
        {
            return BitConverter.ToBoolean(value, startIndex);
        }

        /// <summary>转换为基础数据类型
        /// </summary>
        public static char ToChar(this byte[] value, int startIndex)
        {
            return BitConverter.ToChar(value, startIndex);
        }

        /// <summary>转换为基础数据类型
        /// </summary>
        public static double ToDouble(this byte[] value, int startIndex)
        {
            return BitConverter.ToDouble(value, startIndex);
        }
        /// <summary>转换为基础数据类型
        /// </summary>
        public static float ToSingle(this byte[] value, int startIndex)
        {
            return BitConverter.ToSingle(value, startIndex);
        }
        /// <summary>转换为基础数据类型
        /// </summary>
        public static ushort ToUInt16(this byte[] value, int startIndex)
        {
            return BitConverter.ToUInt16(value, startIndex);
        }

        /// <summary>转换为指定编码的字符串
        /// </summary>
        public static string Decode(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        /// <summary>使用指定算法Hash  
        /// </summary>
        public static byte[] Hash(this byte[] data, string hashName)
        {
            var algorithm = string.IsNullOrEmpty(hashName) ? HashAlgorithm.Create() : HashAlgorithm.Create(hashName);
            if (algorithm != null)
                return algorithm.ComputeHash(data);
            return null;
        }

        /// <summary>使用默认算法Hash
        /// </summary>
        public static byte[] Hash(this byte[] data)
        {
            return Hash(data, null);
        }

        /// <summary>位运算:获取取第index是否为1  
        /// </summary>
        public static bool GetBit(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }

        /// <summary>位运算:将第index位设为1  
        /// </summary>
        public static byte SetBit(this byte b, int index)
        {
            b |= (byte) (1 << index);
            return b;
        }

        /// <summary>位运算:将第index位设为0  
        /// </summary>
        public static byte ClearBit(this byte b, int index)
        {
            b &= (byte) ((1 << 8) - 1 - (1 << index));
            return b;
        }

        /// <summary>位运算:将第index位取反 
        /// </summary>
        public static byte ReverseBit(this byte b, int index)
        {
            b ^= (byte) (1 << index);
            return b;
        }

        /// <summary>保存为文件
        /// </summary>
        public static void Save(this byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }

        /// <summary>
        /// 报告指定的字节数组在源数组中的第一个匹配项的索引.nknife
        /// </summary>
        /// <param name="data">源数组</param>
        /// <param name="target">指定的字节数组</param>
        /// <param name="position">开始匹配的位置</param>
        /// <returns>索引值。为-1时，指无匹配项。</returns>
        public static int Find(this byte[] data, byte[] target, int position = 0)
        {
            if (UtilityCollection.IsNullOrEmpty(target))
                return -1;
            int i;

            for (i = position; i < data.Length; i++)
            {
                if (i + target.Length <= data.Length)
                {
                    int j;
                    for (j = 0; j < target.Length; j++)
                    {
                        if (data[i + j] != target[j]) 
                            break;
                    }

                    if (j == target.Length)
                        return i;
                }
                else
                {
                    break;
                }
            }
            return -1;
        }

        /// <summary>转换为内存流
        /// </summary>
        public static MemoryStream ToMemoryStream(this byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}