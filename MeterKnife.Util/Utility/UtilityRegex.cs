using System.Text.RegularExpressions;
using MeterKnife.Util.ShareResources;

namespace NKnife.Utility
{
    public class UtilityRegex
    {
        /// <summary>
        /// 正则：验证邮件地址。
        /// 正则表达式来自：http://RegexLib.com
        /// </summary>
        static public Regex EmailAddress
        {
            get { return new Regex(RegexString.RegexStr_SimpleEmail, RegexOptions.Multiline | RegexOptions.ExplicitCapture); }
        }
        /// <summary>
        /// 正则：验证Url地址。
        /// </summary>
        static public Regex HttpUrl
        {
            get { return new Regex(RegexString.RegexStr_HttpUrl); }
        }
        /// <summary>
        /// 正则：回车符“\r\n”。
        /// </summary>
        static public Regex Br
        {
            get { return new Regex(RegexString.RegexStr_Br, RegexOptions.IgnoreCase); }
        }
        /// <summary>
        /// 正则：yy-mm-dd字符串。
        /// </summary>
        static public Regex Date
        {
            get { return new Regex(RegexString.RegexStr_Date); }
        }
        /// <summary>
        /// 正则：00:00:00字符串。
        /// </summary>
        static public Regex Time
        {
            get { return new Regex(RegexString.RegexStr_Time); }
        }

        /// <summary>
        /// 正则：规范的文件名
        /// </summary>
        static public Regex FileName
        {
            get { return new Regex(RegexString.RegexStr_FileName); }
        }

    }
}
