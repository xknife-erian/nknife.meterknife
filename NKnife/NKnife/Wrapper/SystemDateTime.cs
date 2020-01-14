using System;
using System.Runtime.InteropServices;
using NKnife.API;

namespace NKnife.Wrapper
{
    /// <summary>
    /// 系统时间的修改。
    /// </summary>
    public class SystemDateTime
    {
        [DllImport("Kernel32.dll")]
        private static extern Boolean SetLocalTime(ref API.API.SysDateTime st);

        [DllImport("kernel32.dll")]
        public static extern void GetLocalTime(ref API.API.SysDateTime lpSystemTime);

        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="newDateTime">新时间</param>
        /// <returns></returns>
        public static bool SetLocalTime(DateTime newDateTime)
        {
            var st = new API.API.SysDateTime
                         {
                             Year = Convert.ToUInt16(newDateTime.Year),
                             Month = Convert.ToUInt16(newDateTime.Month),
                             Day = Convert.ToUInt16(newDateTime.Day),
                             DayOfWeek = Convert.ToUInt16(newDateTime.DayOfWeek),
                             Hour = Convert.ToUInt16(newDateTime.Hour),
                             Minute = Convert.ToUInt16(newDateTime.Minute),
                             Second = Convert.ToUInt16(newDateTime.Second),
                             MilliSeconds = Convert.ToUInt16(newDateTime.Millisecond)
                         };
            return SetLocalTime(ref st);
        }

    }
}