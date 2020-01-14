using System.Text;

namespace NKnife.Encrypt
{
    public class SoftwareSN
    {
        /// <summary>
        /// 一个非常简单的通过取余计算注册码的方法
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string GetSN(string str)
        {
            string strResult = "";//作为返回的字符串
            var sbTemp = new StringBuilder();
            //生成注册码
            for (int i = 0; i < str.Length; i++)
            {
                int tIn = str[i] % 10;
                string tCh = tIn.ToString();
                sbTemp.Append(tCh);
            }
            strResult = sbTemp.ToString();
            return strResult;
        }
    }
}
