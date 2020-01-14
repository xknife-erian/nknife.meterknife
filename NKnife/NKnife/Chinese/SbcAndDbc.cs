using System;
using System.Text;

namespace NKnife.Chinese
{
    /// <summary>全角字符与半角字符相关的帮助类
    /// </summary>
    public class SbcAndDbc
    {
        #region "常量"

        /// <summary>最大的有效全角英文字符转换成int型数据的值
        /// </summary>
        private const int MAX_SBC = 65374;

        /// <summary>最小的有效全角英文字符转换成int型数据的值
        /// </summary>
        private const int MIN_SBC = 65281;

        /// <summary>最大的有效半角英文字符转换成int型数据的值
        /// </summary>
        private const int MAX_DBC = 126;

        /// <summary>最小的有效半角英文字符转换成int型数据的值
        /// </summary>
        private const int MIN_DBC = 33;

        /// <summary>对应的全角和半角的差
        /// </summary>
        private const int MARGIN = 65248;

        #endregion

        #region "全角转换为半角"

        /// <summary>
        /// 功能:全角转换为半角
        /// </summary>
        /// <param name="originalStr">要进行全角到半角转换的字符串</param>
        /// <param name="start">要进行全角到半角转换的开始位置,不能大于end</param>
        /// <param name="end">要进行全角到半角转换的结束位置,不能小于start</param>
        /// <returns>转换成对应半角的字符串</returns>
        public static string ToDBC(string originalStr, int start, int end)
        {
            /*空字符串返回空*/
            if (originalStr == "")
                return "";

            var sb = new StringBuilder();
            for (int i = 0; i < originalStr.Length; i++)
            {
                if (i >= start && i <= end)
                    sb.Append(ToDBC(originalStr[i]));
                else
                    sb.Append(originalStr[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 功能:全角转换为半角
        /// </summary>
        /// <param name="originalStr">要进行全角到半角转换的字符串</param>
        /// <returns>转换成对应半角的字符串</returns>
        public static string ToDBC(string originalStr)
        {
            return ToDBC(originalStr, 0, originalStr.Length - 1);
        }

        /// <summary>
        /// 功能:将全角字符转换为半角字符
        /// </summary>
        /// <param name="originalChar">要进行全角到半角转换的字符</param>
        /// <returns>全角字符转换为半角后的字符</returns>
        public static char ToDBC(char originalChar)
        {
            /*空格是特殊的,其全角和半角的差值也与其他字符不同*/
            if (originalChar == '　')
                return ' ';
            if (originalChar >= MIN_SBC && originalChar <= MAX_SBC)
                return (char) (originalChar - MARGIN);
            return originalChar;
        }

        #endregion

        #region "半角转换为全角"

        /// <summary>
        /// 功能:半角转换为全角
        /// </summary>
        /// <param name="originalStr">要进行半角到全角转换的字符串</param>
        /// <param name="start">要进行半角到全角转换的开始位置,不能大于end</param>
        /// <param name="end">要进行半角到全角转换的结束位置,不能小于start</param>
        /// <returns>转换成对应全角的字符串</returns>
        public static string ToSBC(string originalStr, int start, int end)
        {
            if (start < 0 || end < 0)
                throw new Exception("开始位置或结束位置不能小于零");

            if (start > end)
                throw new Exception("开始位置不能大于结束位置");

            if (start >= originalStr.Length || end >= originalStr.Length)
                throw new Exception("开始位置或结束位置必须小于字符串的最大长度");

            if (originalStr == "")
                return "";

            var sb = new StringBuilder();
            for (int i = 0; i < originalStr.Length; i++)
            {
                if (i >= start && i <= end)
                    sb.Append(ToSBC(originalStr[i]));
                else
                    sb.Append(originalStr[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 功能:半角转换为全角
        /// </summary>
        /// <param name="originalStr">要进行半角到全角转换的字符串</param>
        /// <returns>转换成对应全角的字符串</returns>
        public static string ToSBC(string originalStr)
        {
            return ToSBC(originalStr, 0, originalStr.Length - 1);
        }

        /// <summary>
        /// 功能:半角转换为全角
        /// </summary>
        /// <param name="originalChar">要进行半角到全角转换的字符</param>
        /// <returns>半角字符转换为全角后的字符</returns>
        public static char ToSBC(char originalChar)
        {
            /*空格是特殊的,其全角和半角的差值也与其他字符不同*/
            if (originalChar == ' ')
                return '　';
            if (originalChar >= MIN_DBC && originalChar <= MAX_DBC)
                return (char) (originalChar + MARGIN);
            return originalChar;
        }

        #endregion

        #region "全角半角互换"

        /// <summary>
        /// 功能:将字符串中的全角字符转换为半角,半角字符转换为全角
        /// </summary>
        /// <param name="originalStr">要进行全角半角互换的字符串</param>
        /// <param name="start">要进行全角半角互换字符串的开始位置,不能大于end</param>
        /// <param name="end">要进行全角半角互换字符串的结束位置,不能小于start</param>
        /// <returns>全角半角互换后的字符串</returns>
        public static string Exchange(string originalStr, int start, int end)
        {
            #region "异常判断"

            if (start < 0 || end < 0)
                throw new ArgumentException("开始位置或结束位置不能小于零");

            if (start > end)
                throw new ArgumentException("开始位置不能大于结束位置");

            if (start >= originalStr.Length || end >= originalStr.Length)
                throw new ArgumentException("开始位置或结束位置必须小于字符串的最大长度");

            #endregion

            if (originalStr == "")
                return "";
            var sb = new StringBuilder();
            for (int i = 0; i < originalStr.Length; i++)
            {
                if (i >= start && i <= end)
                    sb.Append(Exchange(originalStr[i]));
                else
                    sb.Append(originalStr[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 功能:将字符串中的全角字符转换为半角,半角字符转换为全角
        /// </summary>
        /// <returns>全角半角互换后的字符串</returns>
        public static string Exchange(string originalStr)
        {
            return Exchange(originalStr, 0, originalStr.Length - 1);
        }

        /// <summary>
        /// 功能:全角字符和半角字符互换
        /// </summary>
        /// <param name="originalChar">要进行全角和半角互换的字符</param>
        /// <returns>行全角和半角互换后的字符</returns>
        public static char Exchange(char originalChar)
        {
            if (originalChar == ' ')
                return '　';
            if (originalChar == '　')
                return ' ';
            if (originalChar >= MIN_SBC && originalChar <= MAX_SBC)
                return (char) (originalChar - MARGIN);
            if (originalChar >= MIN_DBC && originalChar <= MAX_DBC)
                return (char) (originalChar + MARGIN);
            return originalChar;
        }

        #endregion
    }
}