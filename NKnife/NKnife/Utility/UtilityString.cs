using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace NKnife.Utility
{
    /// <summary>
    /// 有关字符串String的扩展方法
    /// </summary>
    public static class UtilityString
    {
        public static string TidyUTF8(byte[] data)
        {
            string receive;
            if (data[0] == 239 && data[1] == 187 && data[2] == 191)
            {
                receive = Encoding.UTF8.GetString(data, 3, data.Length - 3);
            }
            else
            {
                receive = Encoding.UTF8.GetString(data);
            }
            return receive;
        }

        public static string TidyUTF8(string data)
        {
            byte[] reB = Encoding.UTF8.GetBytes(data);
            if (reB[0] == 239 && reB[1] == 187 && reB[2] == 191)
            {
                return Encoding.UTF8.GetString(reB, 3, reB.Length - 3);
            }
            return data;
        }

        /// <summary>
        /// 根据对象的属性与属性值生成字符串
        /// </summary>
        public static string ToString(object o)
        {
            Type t = o.GetType();
            var sb = new StringBuilder();

            PropertyInfo[] pi = t.GetProperties();

            sb.Append("Properties for: " + o.GetType().Name + Environment.NewLine);
            foreach (PropertyInfo i in pi)
            {
                try
                {
                    sb.Append("\t" + i.Name + "(" + i.PropertyType + "): ");
                    if (null != i.GetValue(o, null))
                    {
                        sb.Append(i.GetValue(o, null));
                    }
                }
                catch
                {
                    Debug.Fail(string.Format("属性“{0}”取值时异常", i.Name));
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 针对【M1#个人业务&M5#VIP业务&M2#对公业务】格式的数据进行转换
        /// </summary>
        public static Dictionary<string, string> SplitXX(string msg)
        {
            var typeMap = new Dictionary<string, string>();
            string[] kv = msg.Split('&');
            foreach (string item in kv)
            {
                if (String.IsNullOrEmpty(item))
                {
                    continue;
                }
                string[] ab = item.Split('#');
                if (typeMap.ContainsKey(ab[0]))
                {
                    continue;
                }
                if (ab.Length == 2)
                {
                    typeMap.Add(ab[0], ab[1]);
                }
                else if (ab.Length > 2)
                {
                    var rab = new StringBuilder();
                    for (int i = 1; i < ab.Length; i++)
                    {
                        if (String.IsNullOrEmpty(ab[i]))
                            continue;
                        rab.Append(ab[i]).Append("#");
                    }
                    typeMap.Add(ab[0], rab.ToString().TrimEnd('#'));
                }
                else
                {
                    typeMap.Add(ab[0], "");
                }
            }
            return typeMap;
        }

        /// <summary>
        /// 区位码及汉字之间的互换
        /// </summary>
        /// <param name="character">汉字</param>
        /// <returns></returns>
        public static string CharacterToCoding(string character)
        {
            string coding = "";
            for (int i = 0; i < character.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(character.Substring(i, 1)); //取出二进制编码内容
                string lowCode = Convert.ToString(bytes[0], 16); //取出低字节编码内容（两位16进制）
                if (lowCode.Length == 1)
                    lowCode = "0" + lowCode;
                string hightCode = Convert.ToString(bytes[1], 16); //取出高字节编码内容（两位16进制）
                if (hightCode.Length == 1)
                    hightCode = "0" + hightCode;
                coding += (lowCode + hightCode); //加入到字符串中,
            }
            return coding;
        }

        /// <summary>
        /// 区位码及汉字之间的互换
        /// </summary>
        /// <param name="coding">区位码</param>
        /// <returns></returns>
        public static string CodingToCharacter(string coding)
        {
            string characters = "";
            if (coding.Length%4 != 0) //编码为16进制,必须为4的倍数。
            {
                throw new Exception("编码格式不正确");
            }
            for (int i = 0; i < coding.Length; i += 4) //每四位为一个汉字
            {
                var bytes = new byte[2];
                string lowCode = coding.Substring(i, 2); //取出低字节,并以16进制进制转换
                bytes[0] = Convert.ToByte(lowCode, 16);
                string highCode = coding.Substring(i + 2, 2); //取出高字节,并以16进制进行转换
                bytes[1] = Convert.ToByte(highCode, 16);
                string character = Encoding.Unicode.GetString(bytes);
                characters += character;
            }
            return characters;
        }

        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase">全角数字</param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// 转换为简体中文
        /// </summary>
        public static string ToSimplifiedChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        public static string ToTraditionalChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            var h = new Hashtable();

            foreach (string s in strArray)
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }
                h[k.Trim()] = s;
            }

            var result = new string[h.Count];

            h.Keys.CopyTo(result, 0);

            return result;
        }

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }

        /// <summary>
        /// 取指定长度的字符串，字符串如果操过指定长度则将超出的部分用指定字符串代替。
        /// </summary>
        /// <param name="srcString">要检查的字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="length">指定长度</param>
        /// <param name="tailString">用于替换的字符串,可为空</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string srcString, int startIndex, int length, string tailString)
        {
            string myResult = srcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(srcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {
                //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (startIndex >= srcString.Length)
                    {
                        return "";
                    }
                    else
                    {
                        return srcString.Substring(startIndex,
                                                   ((length + startIndex) > srcString.Length) ? (srcString.Length - startIndex) : length);
                    }
                } //if
            } //foreach

            if (length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(srcString);
                //当字符串长度大于起始位置
                if (bsSrcString.Length > startIndex)
                {
                    int endIndex = bsSrcString.Length;
                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (startIndex + length))
                    {
                        endIndex = length + startIndex;
                    }
                    else
                    {
                        //当不在有效范围内时,只取到字符串的结尾
                        length = bsSrcString.Length - startIndex;
                        tailString = "";
                    }
                    int nRealLength = length;
                    var anResultFlag = new int[length];
                    byte[] bsResult = null;
                    int nFlag = 0;
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }
                        anResultFlag[i] = nFlag;
                    }
                    if ((bsSrcString[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                    {
                        nRealLength = length + 1;
                    }
                    bsResult = new byte[nRealLength];
                    Array.Copy(bsSrcString, startIndex, bsResult, 0, nRealLength);
                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + tailString;
                } //if
            } //if (length >= 0)
            return myResult;
        }

        /// <summary>
        /// 判断字符是否汉字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>

        public static bool IsChinese(char c)
        {
            return (int)c >= 0x4E00 && (int)c <= 0x9FA5;
        }

        /// <summary>
        /// 获得字符串（包括汉字）assci码
        /// </summary>
        /// <param name="sSou"></param>
        /// <returns></returns>
        public static string GetAssciHexCode(string sSou)
        {
            byte[] b = Encoding.Default.GetBytes(sSou.ToCharArray());
            return b.Aggregate("", (current, t) => current + Convert.ToString(t, 16));
        }
    }
}