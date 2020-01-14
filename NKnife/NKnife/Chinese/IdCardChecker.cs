using System;

namespace NKnife.Chinese
{
    /// <summary>
    ///     中国大陆地区第二代身份证相关操作函数
    /// </summary>
    public class IdCardChecker
    {
        /// <summary>
        ///     验证身份证号码
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <returns>验证成功为True，否则为False</returns>
        public static bool CheckIdCard(string id)
        {
            if (id.Length == 18)
            {
                bool check = CheckIdCard18(id);
                return check;
            }
            if (id.Length == 15)
            {
                bool check = CheckIdCard15(id);
                return check;
            }
            return false;
        }

        #region 身份证号码验证

        /// <summary>
        ///     验证18位身份证号
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIdCard18(string id)
        {
            long n = 0;
            if (long.TryParse(id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false; //数字验证
            }
            const string ADDRESS = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (ADDRESS.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false; //省份验证
            }
            string birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i])*int.Parse(ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != id.Substring(17, 1).ToLower())
            {
                return false; //校验码验证
            }
            return true; //符合GB11643-1999标准
        }

        /// <summary>
        ///     验证15位身份证号
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIdCard15(string id)
        {
            long n = 0;
            if (long.TryParse(id, out n) == false || n < Math.Pow(10, 14))
            {
                return false; //数字验证
            }
            const string ADDRESS = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (ADDRESS.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false; //省份验证
            }
            string birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证
            }
            return true; //符合15位身份证标准
        }

        #endregion
    }
}