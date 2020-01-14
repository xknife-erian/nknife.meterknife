using System;
using System.Text;
using NKnife.Maths;

namespace NKnife.Chinese
{
    /// <summary>
    /// 一个描述人民币的结构
    /// </summary>
    public struct Rmb
    {
        private readonly int _Digit;
        private readonly int _Number;
        private readonly char _NumberChar;
        private readonly char _UnitChar;

        public Rmb(int digit, char number)
            : this(digit, int.Parse(number.ToString()))
        {
        }

        public Rmb(int digit, string number)
            : this(digit, int.Parse(number))
        {
        }

        public Rmb(int digit, int number)
        {
            _Number = number;
            _Digit = digit;
            _NumberChar = GetNumberChar(number);
            _UnitChar = GetUnitChar(digit);
        }

        /// <summary>
        /// Gets 位的数字.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return _Number; }
        }

        /// <summary>
        /// Gets 第几位.
        /// </summary>
        /// <value>The digit.</value>
        public int Digit
        {
            get { return _Digit; }
        }

        /// <summary>
        /// Gets 转换成的大写字符.
        /// </summary>
        /// <value>The number char.</value>
        public char NumberChar
        {
            get { return _NumberChar; }
        }

        /// <summary>
        /// Gets 转换成的单位字符.
        /// </summary>
        /// <value>The unit char.</value>
        public char UnitChar
        {
            get { return _UnitChar; }
        }

        /// <summary>
        /// Gets 0-9所对应的汉字
        /// </summary>
        public static string ChineseNumber
        {
            get { return "零壹贰叁肆伍陆柒捌玖"; }
        }

        /// <summary>
        /// Gets 数字位所对应的汉字 
        /// </summary>
        public static string ChineseUnit
        {
            get { return "分角元拾佰仟万拾佰仟亿拾佰仟万"; }
        }

        public static bool operator ==(Rmb a, Rmb b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Rmb a, Rmb b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            var r = (Rmb) (obj);
            if (!_Number.Equals(r._Number)) return false;
            if (!_Digit.Equals(r._Digit)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return 27 ^ _Digit ^ _Number;
        }

        /// <summary>
        /// 返回字符串,即使用“F”作为格式符输出。
        /// </summary>
        public override string ToString()
        {
            return ToString("F");
        }

        /// <summary>
        /// 根据格式符要求返回字符串。
        /// 格式符：
        /// D:输出完整意义的人民币大写字符串；
        /// F:输出仅当前位人民币大写字符串，即两位。
        /// </summary>
        /// <param name="format">
        /// 格式符：
        /// D:输出完整意义的人民币大写字符串；
        /// F:输出仅当前位人民币大写字符串，即两位。
        ///</param>
        public string ToString(string format)
        {
            switch (format)
            {
                case "D":
                {
                    if (_Number == 0)
                        return "零元整";
                    var sb = new StringBuilder();
                    sb.Append(_NumberChar).Append(_UnitChar);
                    switch (_Digit)
                    {
                        case 1: //分整
                        case 2: //角整
                        case 3: //元整
                            sb.Append("整");
                            break;
                        case 4: //拾元整
                        case 5: //百元整
                        case 6: //千元整
                        case 7: //万元整
                            sb.Append("元整");
                            break;
                        case 8: //拾万元整
                        case 9: //百万元整
                        case 10: //千万元整
                            sb.Append("万元整");
                            break;
                        case 11: //亿元整
                            sb.Append("元整");
                            break;
                        case 12: //拾亿元整
                        case 13: //百亿元整
                        case 14: //千亿元整
                        case 15: //万亿元整
                            sb.Append("亿元整");
                            break;
                    }
                    return sb.ToString();
                }
                default:
                {
                    if (_Number == 0)
                        return "零";
                    var sb = new StringBuilder(2);
                    sb.Append(_NumberChar).Append(_UnitChar);
                    return sb.ToString();
                }
            }
        }

        public static char GetNumberChar(int number)
        {
            return ChineseNumber[number];
        }

        public static char GetUnitChar(int digit)
        {
            return ChineseUnit[digit - 1];
        }

        public static string ToUpperChineseRmb(string numString, UtilityMath.RoundingMode roundMode = UtilityMath.RoundingMode.Rounding4She5Ru)
        {
            decimal num;
            if (!decimal.TryParse(numString, out num))
                throw new ArgumentException("非数字的字符串。");
            return ToUpperChineseRmb(num, roundMode);
        }

        public static string ToUpperChineseRmb(decimal num, UtilityMath.RoundingMode roundMode = UtilityMath.RoundingMode.Rounding4She5Ru)
        {
            return ToUpperChineseRmb(num);
        }

        /// <summary> 
        /// 将指定的数字转换成人民币的大写形式 
        /// </summary> 
        /// <param name="num">金额.数字型,小于9万亿,大于-9万亿</param>
        /// <returns>返回大写形式</returns> 
        private static string ToUpperChineseRmb(decimal num)
        {
            if (num < 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("应设置金额为正数"));
            }
            num = System.Math.Round(num, 2); //将num取绝对值并四舍五入取2位小数 
            string sourceNum = ((long)(num * 100)).ToString();
            if (sourceNum.Length > 15)
            {
                throw new ArgumentOutOfRangeException(string.Format("金额.数字型,小于9万亿"));
            }

            const string shuzi = "零壹贰叁肆伍陆柒捌玖"; //0-9所对应的汉字 
            string danwei = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string result = ""; //人民币大写金额形式 
            string weiCn = ""; //数字位的汉字读法 
            int nZero = 0; //用来计算连续的零值是几个 

            int index = sourceNum.Length;
            danwei = danwei.Substring(15 - index); //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (int i = 0; i < index; i++)
            {
                string tmpStringNum = sourceNum.Substring(i, 1); //从原num值中取出的值 
                int tmpIntNum = Convert.ToInt32(tmpStringNum); //从原num值中取出的值 
                string numCn; //数字的汉语读法 
                if (i != (index - 3) && i != (index - 7) && i != (index - 11) && i != (index - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (tmpStringNum == "0")
                    {
                        numCn = "";
                        weiCn = "";
                        nZero = nZero + 1;
                    }
                    else
                    {
                        if (tmpStringNum != "0" && nZero != 0)
                        {
                            numCn = "零" + shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                        else
                        {
                            numCn = shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (tmpStringNum != "0" && nZero != 0)
                    {
                        numCn = "零" + shuzi.Substring(tmpIntNum * 1, 1);
                        weiCn = danwei.Substring(i, 1);
                        nZero = 0;
                    }
                    else
                    {
                        if (tmpStringNum != "0" && nZero == 0)
                        {
                            numCn = shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                        else
                        {
                            if (tmpStringNum == "0" && nZero >= 3)
                            {
                                numCn = "";
                                weiCn = "";
                                nZero = nZero + 1;
                            }
                            else
                            {
                                if (index >= 11)
                                {
                                    numCn = "";
                                    nZero = nZero + 1;
                                }
                                else
                                {
                                    numCn = "";
                                    weiCn = danwei.Substring(i, 1);
                                    nZero = nZero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (index - 11) || i == (index - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    weiCn = danwei.Substring(i, 1);
                }
                result = result + numCn + weiCn;

                if (i == index - 1 && tmpStringNum == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    result = result + "整";
                }
            }
            if (num == 0)
            {
                result = "零元整";
            }
            return result;
        }

    }
}