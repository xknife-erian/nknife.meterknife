using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NKnife.API
{
   public sealed partial class API
    {
       /// <summary>
        /// 面向C#使用API的封装:对Kernel32.dll的封装
       /// </summary>
       public class Kernel32
       {
           #region WINUSER.H

           public static int WM_USER = 0x0400;

           #endregion

           [DllImport("kernel32.dll", SetLastError = true)]
           public static extern int SetLocalTime(ref SysDateTime lpSystemTime);

           [DllImport("kernel32.dll", SetLastError = true)]
           public static extern int GetLocalTime(ref SysDateTime lpSystemTime);
       }

       /// <summary>
       /// 系统时间的结构封装
       /// </summary>
       [StructLayout(LayoutKind.Sequential)]
       public struct SysDateTime : IEquatable<SysDateTime>
       {
           public ushort Year;
           public ushort Month;
           public ushort DayOfWeek;
           public ushort Day;
           public ushort Hour;
           public ushort Minute;
           public ushort Second;
           public ushort MilliSeconds;

           public override bool Equals(object obj)
           {
               if (ReferenceEquals(null, obj)) return false;
               if (obj.GetType() != typeof(SysDateTime)) return false;
               return Equals((SysDateTime)obj);
           }

           public bool Equals(SysDateTime other)
           {
               return other.Year == Year && other.Month == Month && other.DayOfWeek == DayOfWeek && other.Day == Day && other.Hour == Hour && other.Minute == Minute && other.Second == Second && other.MilliSeconds == MilliSeconds;
           }

           public override int GetHashCode()
           {
               unchecked
               {
                   int result = Year.GetHashCode();
                   result = (result * 397) ^ Month.GetHashCode();
                   result = (result * 397) ^ DayOfWeek.GetHashCode();
                   result = (result * 397) ^ Day.GetHashCode();
                   result = (result * 397) ^ Hour.GetHashCode();
                   result = (result * 397) ^ Minute.GetHashCode();
                   result = (result * 397) ^ Second.GetHashCode();
                   result = (result * 397) ^ MilliSeconds.GetHashCode();
                   return result;
               }
           }

           public static bool operator ==(SysDateTime left, SysDateTime right)
           {
               return left.Equals(right);
           }

           public static bool operator !=(SysDateTime left, SysDateTime right)
           {
               return !left.Equals(right);
           }

           public override string ToString()
           {
               return string.Format("{0}-{1}-{2}({3}) {4}:{5}:{6}:{7}",
                                    Year, Month, Day, DayOfWeek, Hour, Second, Minute, MilliSeconds);
           }
       }
    }
}
