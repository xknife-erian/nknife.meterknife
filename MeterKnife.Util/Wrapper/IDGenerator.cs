using System;
using System.Collections.Generic;
using System.Text;
using MeterKnife.Util.Interface;
using MeterKnife.Util.Maths;

namespace MeterKnife.Util.Wrapper
{
    /// <summary>
    /// 由本框架定义思想构建的ID生成器。本生成器主要思想是将当前时间中的年月日时分替换成一个在当年不可重
    /// 复的4位（大写字母与数字）字符串标识，这样相对存储长格式时间字符串以及GUID方法来讲，可以大大减少存储空间。
    /// * 本类可以直接使用。但建议不要直接使用此类，而通过继承该类，重写ID中各部份输出规则后再使用。
    /// * 本类线程是安全的。
    /// * 本类直接使用时，以每6秒为单位，假定每6秒全局生成记录不超过1000条，这样全局记录将不会重复，如应用负载较大时请重写GetSecond与GetCount方法。
    /// * 2010-01-18 11:36:29
    /// 
    /// 经比较，ID生成效率大约为如下所示：
    /// Gean.Data的Id生成器生成500万个ID需要时间（毫秒）： 26090，每秒生成：191000
    /// 随机数字生成500万个ID需要时间（毫秒）： 2726，每秒生成：1834000
    /// 随机4位小写字母生成500万个ID需要时间（毫秒）： 43732，每秒生成：114000
    /// 数字累加生成500万个ID需要时间（毫秒）： 2399，每秒生成：2084000
    /// Hibernate的UUIDGenerator生成生成500万个ID需要时间（毫秒）： 60331，每秒生成：82000
    /// Guid生成500万个ID需要时间（毫秒）： 5182，每秒生成：964000
    /// </summary>
    public class IdGenerator : IIdGenerator
    {
        /// <summary>
        /// 累计数，消除当以秒为单位时出现重复的可能。
        /// </summary>
        protected static Int32 _Counter = 0;
        /// <summary>
        /// 将当前时间中的年月日时分替换成一个在当年里不可重复的4位（大写字母与数字）字符串标识。
        /// </summary>
        protected static List<string> _TimeFlag;

        /// <summary>
        /// 静态构造函数。
        /// </summary>
        static IdGenerator()
        {
            FillTimeFLag();
        }

        /// <summary>
        /// 填充时间标识符数组
        /// </summary>
        private static void FillTimeFLag()
        {
            const int timeLength = 366 * 24 * 60;
            char[] src = "ABCDEFGHJKLMNPQRSTWXY23456789".ToCharArray();//减除不易识读的：I,O,U,V,Z,0,1
            _TimeFlag = new List<string>(timeLength);
            var permut = new Permutations<char>(src, 4);
            int i = 0;
            foreach (char[] charArray in permut)
            {
                if (i >= timeLength) return;
                var sb = new StringBuilder();
                foreach (char c in charArray)
                {
                    sb.Append(c);
                }
                _TimeFlag.Add(sb.ToString());
                i++;
            }
        }

        /// <summary>
        /// 获得一个用户标志符，默认为空。可在子类中重写输出。
        /// </summary>
        /// <returns></returns>
        protected virtual string GetUserFlag()
        {
            return "";
        }
        /// <summary>
        /// 获得一个序列名，默认为空。可在子类中重写输出。
        /// </summary>
        /// <returns></returns>
        protected virtual string GetSequenceName()
        {
            return "";
        }
        /// <summary>
        /// 获得一个时间标志符（4位）。年份标识追加在标志符后,默认是当前年份距2010年的相差数+1。可在子类中重写输出。
        /// </summary>
        /// <returns></returns>
        protected virtual string GetDateTimeFlag()
        {
            var sb = new StringBuilder();

            //获得一个时间标志符（4位）
            int time = DateTime.Now.DayOfYear * (DateTime.Now.Hour + 1) * (DateTime.Now.Minute + 1);
            sb.Append(_TimeFlag[time]);

            //年份标识追加在标志符后,默认是当前年份距2010年的相差数+1。
            time = DateTime.Now.Year;
            time = time - 2010;
            time = time + 1;
            sb.Append(time);

            return sb.ToString();
        }
        /// <summary>
        /// 获得当前时间的秒除以10的值，即以每6秒为一组计时单位。可在子类中重写输出。
        /// </summary>
        /// <returns></returns>
        protected virtual string GetSecond()
        {
            return Convert.ToString((int)(DateTime.Now.Second / 6));
        }
        /// <summary>
        /// 获得一个累计数，以消除当以秒为单位时出现重复的可能。可在子类中重写输出。
        /// <code>
        /// int n = 4; //希望仅出现4位计数标志
        /// if (_counter &gt; GetMaxCount(n) - 1)
        ///     _counter++;
        /// else
        ///     _counter = 1;
        /// return Convert.ToString(_counter).PadLeft(n, '0');
        /// </code>
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCount()
        {
            int n = 3; //希望仅出现3位计数标志
            if (_Counter < GetMaxCount(n) - 1)
                _Counter++;
            else
                _Counter = 1;
            return Convert.ToString(_Counter).PadLeft(n, '0');
        }

        /// <summary>
        /// 主方法。根据当前类的输出规则生成一个全局不重复的ID。
        /// </summary>
        /// <returns>一个全局不重复的ID字符串</returns>
        public virtual string Generate()
        {
            return (new StringBuilder())
                .Append(GetUserFlag())      //用户标志，可以定义为不同系统的标志
                .Append(GetSequenceName())  //序列名，如可以定义为序列名
                .Append(GetDateTimeFlag())  //时间的标志位
                .Append(GetSecond())        //秒
                .Append(GetCount())         //一个数字的累加
                .ToString();
        }

        /// <summary>
        /// Gets the max count.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        protected int GetMaxCount(int n)
        {
            int max = 1;
            for (int i = 0; i < n; i++)
            {
                max *= 10;
            }
            return max;
        }

    }
}
