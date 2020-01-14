using System;
using System.Collections.Generic;
using System.Text;

namespace NKnife.Maths
{
    public class UtilityMath
    {
        #region RoundingMode enum

        /// <summary>舍入的方式，指按“四舍五入”，或“四舍六入五成双”
        /// </summary>
        public enum RoundingMode
        {
            /// <summary>
            /// 四舍五入
            /// </summary>
            Rounding4She5Ru,

            /// <summary>
            /// 四舍六入五成双(System.Math.Round的默认方式)。
            /// Math.Round(1.25, 1) = 1.2 因为5前面是2,为偶数,所以把5舍去不进位。
            /// Math.Round(1.35, 1) = 1.4 因为5前面是3,为奇数,所以进位。
            /// 而0也看成为偶数,所以Math.Round(0.5, 0) = 0。
            /// 从统计学的角度,"四舍六入五成双"比"四舍五入"要科学,它使舍入后的结果有的变大,有的变小,更
            /// 平均.而不是像四舍五入那样逢五就入,导致结果偏向大数。
            /// 例如:1.15+1.25+1.35+1.45=5.2,若按四舍五入取一位小数计算1.2+1.3+1.4+1.5=5.4。
            /// 按"四舍六入五成双"计算,1.2+1.2+1.4+1.4=5.2,舍入后的结果更能反映实际结果。
            /// </summary>
            Rounding4She6Ru5Chengshuang
        }

        #endregion

        /// <summary>返回一个序列数，如果指定序列中的数是连续的，那么返回的是这个序列中的最大的数加一，如果不是连续的，将返回第一个填空的数。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="first"></param>
        /// <returns></returns>
        public static int GetSequenceNumber<T>(List<T> list, int first = 1)
        {
            list.Sort();
            int i = 0;
            int n = 0;
            while (i < list.Count)
            {
                var v = list[i];
                if (int.TryParse(v.ToString(), out n))
                {
                    if (first < n)
                        return first;
                }
                else
                {
                    return first;
                }
                first++;
                i++;
            }
            if (first < n)
                return first;
            return n + 1;
        }

        /// <summary>返回一个整数的位数
        /// </summary>
        public static uint GetLength(int number)
        {
            uint length = 0;
            while (number > 0)
            {
                number /= 10;
                ++length;
            }
            return length;
        }

        /// <summary>按照四舍五入的规则进行舍位。
        /// 因为Math.Round方法并不是遵循四舍五入的原则，而是采用“四舍六入五成双”这种方式，若需要
        /// 舍入到的位的后面"小于5"或"大于5"的话,按通常意义的四舍五入处理。若"若需要舍入到的位的
        /// 后面"等于5",则要看舍入后末位为偶数还是奇数。
        /// </summary>
        /// <param name="d"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal Round(decimal d, int decimals)
        {
            decimal tenPow = Convert.ToDecimal(System.Math.Pow(10, decimals));
            decimal scrD = d*tenPow + 0.5m;
            return (Convert.ToDecimal(System.Math.Floor(Convert.ToDouble(scrD)))/tenPow);
        }

        /// <summary>将一个字符类型的数字加1，如字符串"20060308"，加1后变成"20060309"。
        /// 如果字符串是"0002006"，"JH200603"，甚至是"JH20060A01"，这个函数是
        /// 将字符串的ASCII码+1，所以基本不需考虑是否包含字母或包含0前缀。 
        /// </summary>
        /// <param name="numberStr">The number STR.</param>
        /// <returns></returns>
        public static string IncString(string numberStr)
        {
            if (numberStr == "") return "";

            char[] numberChar = numberStr.ToCharArray();
            bool isJw = true; //进位
            int charIndex = numberStr.Length - 1;
            string resultStr = "";

            while (charIndex >= 0)
            {
                if (isJw)
                {
                    isJw = false;
                    if (numberStr[charIndex] == '9' || numberStr[charIndex] == 'Z' || numberStr[charIndex] == 'z')
                    {
                        resultStr = "0" + resultStr;
                        isJw = true;
                    }
                    else
                    {
                        int asciiValue = numberStr[charIndex] + 1;
                        var ascii = (char) asciiValue;
                        resultStr = new string(ascii, 1) + resultStr;
                    }
                }
                else
                {
                    resultStr = new string(numberStr[charIndex], 1) + resultStr;
                }
                charIndex--;
            }
            return resultStr;
        }

        /// <summary>连乘积函数
        /// 类似：1 x 2 x 3 x 4 ……
        /// </summary>   
        /// <param name="start">起点数(较小的数字)</param>
        /// <param name="end">终点数(较大的数字)</param>
        public static BigInteger ContinuousMultiplication(int start, int end)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(string.Format("{0} can not be less than 0", start));
            if (end < 0)
                throw new ArgumentOutOfRangeException(string.Format("{0} can not be less than 0", end));
            if (start > end)
                throw new ArgumentOutOfRangeException(string.Format("{0} compare {1} large", start, end));
            if (end == 0)
                return 1;

            BigInteger tempResult = 1;
            for (int i = start; i <= end; i++)
            {
                tempResult *= i;
            }
            return tempResult;
        }

        /// <summary>阶乘函数。
        /// 也就是求解将n个相异物排成一列的排列数。
        /// 阶乘(factorial)是基斯顿·卡曼(Christian Kramp, 1760 – 1826)于1808年发明的运算符号。
        /// 阶乘指从1乘以2乘以3乘以4一直乘到所要求的数。 
        /// 即为限定连乘积函数从“0”开始。
        /// </summary>
        /// <param name="n">要求的阶乘数</param>
        /// <returns></returns>
        public static BigInteger Factorial(int n)
        {
            return ContinuousMultiplication(1, n);
        }

        /// <summary>排列数函数
        /// 公式P是指排列，从N个元素取R个进行排列。
        /// </summary>
        public static BigInteger P(int n, int r)
        {
            if (1 > r || r > n)
            {
                throw new ArgumentOutOfRangeException("N >= R >= 1");
            }
            return ContinuousMultiplication(n - r + 1, n);
        }

        /// <summary>组合数函数
        /// 公式C是指组合，从N个元素取R个进行组合。
        /// </summary>
        /// <param name="n"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static BigInteger C(int n, int r)
        {
            if (1 > r || r > n)
            {
                throw new ArgumentOutOfRangeException("N >= R >= 1");
            }
            BigInteger a = ContinuousMultiplication(n - r + 1, n);
            BigInteger b = ContinuousMultiplication(2, r);
            return a/b;
        }

        /// <summary>[否决的，未完成的] 排列循环方法  
        /// </summary>  
        /// <param name="N"></param>  
        /// <param name="R"></param>  
        /// <returns></returns>  
        public static long _test_P1(int N, int R)
        {
            if (R > N || R <= 0 || N <= 0) throw new ArgumentException("params invalid!");
            long t = 1;
            int i = N;

            while (i != N - R)
            {
                try
                {
                    checked
                    {
                        t *= i;
                    }
                }
                catch
                {
                    throw new OverflowException("overflow happens!");
                }
                --i;
            }
            return t;
        }

        /// <summary>[否决的，未完成的] 排列堆栈方法  
        /// </summary>  
        /// <param name="N"></param>  
        /// <param name="R"></param>  
        /// <returns></returns>  
        public static long _test_P2(int N, int R)
        {
            if (R > N || R <= 0 || N <= 0) throw new ArgumentException("arguments invalid!");
            var s = new Stack<int>();
            long iRlt = 1;
            int t;
            s.Push(N);
            while ((t = s.Peek()) != N - R)
            {
                try
                {
                    checked
                    {
                        iRlt *= t;
                    }
                }
                catch
                {
                    throw new OverflowException("overflow happens!");
                }
                s.Pop();
                s.Push(t - 1);
            }
            return iRlt;
        }

        /// <summary>[否决的，未完成的] 组合  
        /// </summary>  
        /// <param name="N"></param>  
        /// <param name="R"></param>  
        /// <returns></returns>  
        public static long _test_C(int N, int R)
        {
            return _test_P1(N, R)/_test_P1(R, R);
        }

        /// <summary>
        /// [否决的，未完成的] 
        /// 10置换法完成组合
        /// 算法思想：
        /// (1)  初始化一个m个元素的数组（全部由0，1组成），将前n个初始化为1，后面的为0。这时候就可以输出第一个组合序列了。
        /// (2)  从前往后找，找到第一个10组合，将其反转成01，然后将这个10组合前面的所有1，全部往左边推 ，
        /// 即保证其前面的1都在最左边。这时又可以输出一组组合序列了。
        /// (3)  重复第(2)步，知道找不到10组合位置。这时已经输出了全部的可能序列了。 
        /// 为什么？你想，（以m=5,n=3为例）一开始是11100，最后就是00111，已经没有10组合了 。 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        public void _test_combination(int m, int n)
        {
            var totalArray = new char[m];
            //记录排序次数  
            int totalSortNum = 0;

            //建立 111...100...0  
            for (int i = 0; i < m; i++)
            {
                if (i < n)
                    totalArray[i] = '1';
                else
                    totalArray[i] = '0';
            }
            totalSortNum += 1;

            //"10"反转置换法  
            int index = -1;
            while ((index = ArrayToString(totalArray).IndexOf("10")) != -1)
            {
                //交换"10"为"01"  
                totalArray[index] = '0';
                totalArray[index + 1] = '1';
                //计算刚反转的"10"前面所有的'1'全部移动到最左边  
                int count = 0;
                for (int i = 0; i < index; i++)
                {
                    if (totalArray[i] == '1')
                        count++;
                }
                for (int j = 0; j < index; j++)
                {
                    if (j < count)
                        totalArray[j] = '1';
                    else totalArray[j] = '0';
                }
                //输出结果  
                totalSortNum++;
            }
        }

        private string ArrayToString(IEnumerable<char> cs)
        {
            var sb = new StringBuilder();
            foreach (char c in cs)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}