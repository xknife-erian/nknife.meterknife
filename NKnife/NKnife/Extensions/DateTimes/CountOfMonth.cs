using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>描述某月中的第几周的第几天，如果是第0周，那么 Count 属性描述的是该月的第几天
    /// </summary>
    [Serializable]
    public struct CountOfMonth
    {
        /// <summary>描述当月的第几周，如果是第0周，那么 Count 属性描述的是该月的第几天
        /// </summary>
        /// <value>The week number.</value>
        public int WeekNumber { get; set; }

        /// <summary>第几天
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }

        /// <summary>是否匹配指定的日期
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public bool Matching(DateTime dt)
        {
            if (WeekNumber == 0)
                return Count == dt.Day;
            return WeekNumber == dt.WeekOfMonth() && Count == (int)dt.DayOfWeek;
        }
    }
}
