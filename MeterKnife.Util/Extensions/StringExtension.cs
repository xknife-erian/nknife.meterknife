using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MeterKnife.Util.Utility;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class StringExtension
    {

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="srcString">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayIndex(this string srcString, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (srcString.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (srcString == stringArray[i])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="srcString">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayIndex(this string srcString, string[] stringArray)
        {
            return GetInArrayIndex(srcString, stringArray, true);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="srcString">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string srcString, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayIndex(srcString, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="srcString">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string srcString, string[] stringarray)
        {
            return InArray(srcString, stringarray, false);
        }

        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimTail(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }

        public static string TrimZero(this string str)
        {
            var n = str.LastIndexOf('.');
            if (n > 0)
            {
                bool allIsZero = true;
                int i;
                for (i = str.Length - 1; i >= n + 1; i--)
                {
                    if (str[i] != '0')
                    {
                        allIsZero = false;
                        break;
                    }
                }
                string result;
                if (!allIsZero)
                    result = str.Substring(0, i + 1);
                else
                    result = str.Substring(0, n);
                return result;
            }
            if (str.IsEmptyAndZero())
                return "0";
            return str;
        }

        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string TrimBR(this string str)
        {
            Match m = null;
            for (m = UtilityRegex.Br.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }

        /// <summary>是否是Null,空,全部是空白或全部为0的字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns>
        ///   <c>true</c> if [is composed by zero] [the specified STR]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmptyAndZero(this string str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            int n = str.Length;
            string mStr = "[0]{" + n + ",}";
            return Regex.Match(str, mStr).Success;
        }

        /// <summary>是否是拉丁字母（大小写均可）
        /// </summary>
        public static bool IslLatinLetter(this char c) 
        {
            const string PATTEN = "^[A-Za-z]+$";
            var r = new Regex(PATTEN); 
            Match m = r.Match(c.ToString()); 
            return m.Success;
        }

        /// <summary>
        /// 判断是否邮箱地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string s)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        /// <summary>
        /// 判断是否正整数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsPositiveInteger(this string data, out int result)
        {
            try
            {
                int tempResult;
                if (!int.TryParse(data, out tempResult))
                {
                    result = -1;
                    return false;
                }
                if (tempResult < 1)
                {
                    result = -1;
                    return false;
                }
                result = tempResult;
                return true;
            }
            catch
            {
                result = -1;
                return false;
            }
        }

        /// <summary>
        /// 判断是否正整数，并位于 min 和 max之间
        /// </summary>
        /// <param name="data"></param>
        /// <param name="result"></param>
        /// <param name="min"> </param>
        /// <param name="max"> </param>
        /// <returns></returns>
        public static bool IsInteger(this string data, out int result, int min = 0, int max = int.MaxValue)
        {
            try
            {
                int tempResult;
                if (!int.TryParse(data, out tempResult))
                {
                    result = -1;
                    return false;
                }
                if (tempResult < min || tempResult > max)
                {
                    result = -1;
                    return false;
                }
                result = tempResult;
                return true;
            }
            catch
            {
                result = -1;
                return false;
            }
        }

        /// <summary>
        /// 判断是否为null,empty,或由指定字符组成
        /// </summary>
        /// <param name="data"></param>
        /// <param name="element"> </param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrConsistBy(this string data, char element)
        {
            return string.IsNullOrEmpty(data) || data.All(c => c.Equals(element));
        }

        /// <summary>
        /// 判断是否由数字组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string str)
        {
            var n = str.Length;
            var mStr = "[0-9]{" + n.ToString() + ",}";
            return Regex.Match(str, mStr).Success;
        }

        /// <summary>
        /// 判断是否手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="matchString"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(this string str, string matchString = "")
        {
            string mStr = string.IsNullOrEmpty(matchString) ? @"^[1]+([35]|[86]|[38]|[37])+\d{9}" : matchString;
            return Regex.Match(str, mStr).Success;
        }

        // ~~ 判断字符是否为中文 ~~~~~~~

        /// <summary>
        /// 给定一个字符串，判断其是否是中文字符串
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <returns>
        /// 	<c>true</c> if [is chinese letter] [the specified SRC]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsChineseLetter(this string src)
        {
            for (int i = 0; i < src.Length; i++)
            {
                // \u4e00-\u9fa5 汉字的范围。
                // ^[\u4e00-\u9fa5]$ 汉字的范围的正则
                var rx = new Regex("^[\u4e00-\u9fa5]$");
                if (rx.IsMatch(src.Substring(i, 1)))
                    return true;
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 给定一个字符串，判断其是否仅仅包含有汉字
        /// </summary>
        public static bool IsOnlyContainsChinese(this string srcStr)
        {
            char[] words = srcStr.ToCharArray();
            foreach (char word in words)
            {
                if (IsGBCode(word.ToString()) || IsGBKCode(word.ToString())) // it is a GB2312 or GBK chinese word
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断一个word是否为GB2312编码的汉字
        /// </summary>
        public static bool IsGBCode(this string word)
        {
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(word);
            if (bytes.Length <= 1) // if there is only one byte, it is ASCII code or other code
            {
                return false;
            }
            byte byte1 = bytes[0];
            byte byte2 = bytes[1];
            if (byte1 >= 176 && byte1 <= 247 && byte2 >= 160 && byte2 <= 254) //判断是否是GB2312
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断一个word是否为GBK编码的汉字
        /// </summary>
        /// <param ></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsGBKCode(this string word)
        {
            byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(word);
            if (bytes.Length <= 1) // if there is only one byte, it is ASCII code
            {
                return false;
            }
            byte byte1 = bytes[0];
            byte byte2 = bytes[1];
            if (byte1 >= 129 && byte1 <= 254 && byte2 >= 64 && byte2 <= 254) //判断是否是GBK编码
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断一个word是否为Big5编码的汉字
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsBig5Code(this string word)
        {
            byte[] bytes = Encoding.GetEncoding("Big5").GetBytes(word);
            if (bytes.Length <= 1) // if there is only one byte, it is ASCII code
            {
                return false;
            }
            byte byte1 = bytes[0];
            byte byte2 = bytes[1];
            if ((byte1 >= 129 && byte1 <= 254) && ((byte2 >= 64 && byte2 <= 126) || (byte2 >= 161 && byte2 <= 254))) //判断是否是Big5编码
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断字符串是否能被(filters)过滤
        /// strictMatch=true时，是严格过滤模式，src必须完全等于filters中的某一项，才算Match，return true
        /// strictMatch=false时，是宽松过滤模式，src只要包含filters中的某一项，算Match，return true
        /// </summary>
        /// <param name="src"></param>
        /// <param name="filters"></param>
        /// <param name="strictMatch"></param>
        /// <returns></returns>
        public static bool MatchFilters(this string src, string[] filters, bool strictMatch = false)
        {
            if (filters == null)
                return false;
            foreach (var filter in filters)
            {
                if (strictMatch)
                {
                    if (src.Equals(filter))
                    {
                        return true;
                    }
                }
                else
                {
                    if (src.IndexOf(filter, StringComparison.Ordinal) > -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static byte[] ToBytes(this string src, params char[] separator)
        {
            var strs = src.Split(separator);
            var result = new byte[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                result[i] = (byte) Convert.ToInt32(strs[i], 16);
            }
            return result;
        }
    }
}