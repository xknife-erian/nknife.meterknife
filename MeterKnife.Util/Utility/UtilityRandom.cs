using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NKnife.Maths;

namespace NKnife.Utility
{
    /// <summary>
    /// 针对.net的Random随机数生成器的扩展。
    /// 2008年9月9日16时46分
    /// </summary>
    public class UtilityRandom
    {
        #region RandomCharType enum

        /// <summary>
        /// 枚举：生成的随机字符串（数字与大小写字母）的组合类型。
        /// </summary>
        public enum RandomCharType
        {
            /// <summary>
            /// 任意。数字与大小写字母。
            /// </summary>
            All,

            /// <summary>
            /// 数字。
            /// </summary>
            Number,

            /// <summary>
            /// 大写字母。
            /// </summary>
            Uppercased,

            /// <summary>
            /// 小写字母。
            /// </summary>
            Lowercased,

            /// <summary>
            /// 数字与大写字母。
            /// </summary>
            NumberAndUppercased,

            /// <summary>
            /// 数字与小写字母。
            /// </summary>
            NumberAndLowercased,

            /// <summary>
            /// 小写字母与大写字母。
            /// </summary>
            UppercasedAndLowercased,

            /// <summary>
            /// 嘛也不是
            /// </summary>
            None,
        }

        #endregion

        /// <summary>
        /// 大小写字母与数字(以英文逗号相隔)
        /// </summary>
        private const string CHAR_TO_SPLIT = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        /// <summary>构造函数
        /// </summary>
        static UtilityRandom()
        {
            Random = new Random(unchecked((int) DateTime.Now.Ticks));
        }

        /// <summary>
        /// 表示伪随机数生成器。静态属性。
        /// </summary>
        private static Random Random { get; set; }

        /// <summary>返回非负随机数。
        /// </summary>
        /// <returns>返回大于等于零且小于 System.Int32.MaxValue 的 32 位带符号整数。</returns>
        public int Next()
        {
            return Random.Next();
        }

        /// <summary>返回一个小于所指定最大值的非负随机数。
        /// </summary>
        /// <param name="maxValue">要生成的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于零。</param>
        /// <returns>大于等于零且小于 maxValue 的 32 位带符号整数，即：返回值的范围通常包括零但不包括 maxValue。不过，如果 maxValue 等于零，则返回maxValue。</returns>
        public int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        /// <summary>返回一个指定范围内的随机数。
        /// </summary>
        /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>
        /// <param name="maxValue">返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue。</param>
        /// <returns>一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。如果minValue 等于 maxValue，则返回 minValue。</returns>
        public int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        /// <summary>获取一定数量的随机整数，可能会有重复。
        /// </summary>
        /// <param name="num">需获得随机整数的数量</param>
        /// <param name="minValue">随机整数的最小值</param>
        /// <param name="maxValue">随机整数的最大值</param>
        /// <returns></returns>
        public int[] GetInts(int num, int minValue, int maxValue)
        {
            var ints = new int[num];
            for (int i = 0; i < num; i++)
            {
                ints[i] = Random.Next(minValue, maxValue);
            }
            return ints;
        }

        /// <summary>获取一定数量不重复的随机整数。
        /// </summary>
        /// <param name="num">需获得随机整数的数量</param>
        /// <param name="minValue">随机整数的最小值</param>
        /// <param name="maxValue">随机整数的最大值</param>
        /// <returns></returns>
        public int[] GetUnrepeatInts(int num, int minValue, int maxValue)
        {
            if (num > maxValue - minValue)
            {
                Debug.Fail("num > maxValue - minValue");
            }
            var ints = new List<int>(num);
            for (int i = 0; i < num; i++)
            {
                bool hasValue = false;
                while (!hasValue)
                {
                    int m = Random.Next(minValue, maxValue);
                    if (!ints.Contains(m))
                    {
                        ints.Add(m);
                        hasValue = true;
                    }
                } //while
            } //for
            return ints.ToArray();
        }

        /// <summary>获取指定长度的(单字节)字符串
        /// </summary>
        /// <param name="num">所需字符串的长度</param>
        /// <param name="type">字符串中的字符的类型</param>
        /// <returns></returns>
        public string GetString(int num, RandomCharType type)
        {
            string[] chars = CHAR_TO_SPLIT.Split(',');
            int begin = 0;
            int end = chars.Length;
            switch (type)
            {
                    #region case

                case RandomCharType.Number:
                    end = 11;
                    break;
                case RandomCharType.Uppercased:
                    begin = 10 + 26;
                    break;
                case RandomCharType.Lowercased:
                    begin = 10;
                    end = 10 + 26;
                    break;
                case RandomCharType.NumberAndLowercased:
                    end = 10 + 26;
                    break;
                case RandomCharType.UppercasedAndLowercased:
                    begin = 10;
                    break;
                case RandomCharType.All:
                case RandomCharType.NumberAndUppercased:
                    break;
                default:
                    Debug.Fail(type.ToString());
                    return "";

                    #endregion
            }
            var sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                if (type == RandomCharType.NumberAndUppercased)
                {
                    bool isLow = true;
                    while (isLow) //如果生成的数是小写字母范围的，去除
                    {
                        int m = Random.Next(begin, end);
                        if (!(m >= 10 && m < 10 + 26))
                        {
                            sb.Append(chars[m]);
                            isLow = false;
                        }
                    }
                }
                else
                {
                    sb.Append(chars[Random.Next(begin, end)]);
                }
            }
            return sb.ToString();
        }

        /// <summary>生成随机的银行卡卡号
        /// </summary>
        /// <param name="prefix"> </param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetFixNumberString(int prefix = 0, int length = 16)
        {
            if (length < 1)
                throw new ArgumentException("卡号长度不能小于1");
            var prefixLength = UtilityMath.GetLength(prefix);
            var sb = new StringBuilder(length);
            if (prefix > 0)
            {
                if (prefixLength > length)
                    sb.Append(prefix.ToString().Substring(0, length));
                else
                    sb.Append(prefix);
            }
            for (int i = 0; i < length - prefixLength; i++)
            {
                sb.Append(Random.Next(0, 9));
            }
            return sb.ToString();
        }
    }
}