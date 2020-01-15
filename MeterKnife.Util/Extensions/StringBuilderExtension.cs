using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Text
{
    public static class StringBuilderExtension
    {
        /// <summary>返回本实例的公开枚举器
        /// </summary>
        public static IEnumerable<char> GetEnumerator(this StringBuilder sb)
        {
            for (var i = 0; i < sb.Length; i++)
            {
                yield return sb[i];
            }
        }

        /// <summary>将本实例全部大写
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <returns></returns>
        public static StringBuilder ToUpper(this StringBuilder sb)
        {
            for (var i = 0; i < sb.Length; i++)
            {
                sb[i] = char.ToUpper(sb[i]);
            }
            return sb;
        }

        /// <summary>将本实例全部小写
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <returns></returns>
        public static StringBuilder ToLower(this StringBuilder sb)
        {
            for (var i = 0; i < sb.Length; i++)
            {
                sb[i] = char.ToLower(sb[i]);
            }
            return sb;
        }

        public static int IndexOf(this StringBuilder sb, string str, int startIndex = 0)
        {
            return IndexOf(sb, str.ToCharArray(), startIndex);
        }

        public static int IndexOf(this StringBuilder sb, char[] str, int startIndex = 0)
        {
            if (str != null && str.Length > 0 && !(sb.Length < str.Length))
            {
                for (int i = startIndex; i < (sb.Length - str.Length) + 1; i++)
                {
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (sb[i + j] != str[j])
                            break;
                        if (j == (str.Length - 1))
                            return i;
                    }
                }
            }
            return -1;
        }
    }
}
