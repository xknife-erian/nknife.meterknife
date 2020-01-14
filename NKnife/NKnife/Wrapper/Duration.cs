using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

[assembly: CLSCompliant(true)]

namespace NKnife.Wrapper
{
    /// <summary>
    ///     高精度计时器，主要为衡量“持续时间”而封装的类
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
    public class Duration
    {
        private readonly long _Freq;

        /// <summary>
        ///     高精度计时器是否启动的标记
        /// </summary>
        private bool _RunFlag;

        private double _DurationValue;

        private long _StartTime, _StopTime;

        public Duration()
        {
            if (DurationMethods.QueryPerformanceFrequency(out _Freq) == false)
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        ///     开始点
        /// </summary>
        public DateTime Begin { get; set; }

        /// <summary>
        ///     结束点
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        ///     获取与设置持续时间的值，以毫秒为单位
        /// </summary>
        public virtual double DurationValue
        {
            get
            {
                if (_DurationValue <= 0)
                {
                    _DurationValue = (End - Begin).TotalMilliseconds;
                }
                return _DurationValue;
            }
            set { _DurationValue = value; }
        }

        public static Duration GetDuration(DateTime beginTime, DateTime endTime)
        {
            var dura = new Duration();
            dura.Begin = beginTime;
            dura.End = endTime;
            return dura;
        }

        public static Duration Stop(DateTime beginTime)
        {
            var dur = new Duration();
            dur.Begin = beginTime;
            dur.End = DateTime.Now;
            dur.DurationValue = (dur.End - dur.Begin).TotalMilliseconds;
            return dur;
        }

        public static bool operator ==(Duration aTime, Duration bTime)
        {
            return bTime != null && (aTime != null && aTime.DurationValue.Equals(bTime.DurationValue));
        }

        public static bool operator !=(Duration aTime, Duration bTime)
        {
            return bTime != null && (aTime != null && !(aTime.DurationValue.Equals(bTime.DurationValue)));
        }

        public static Duration operator -(Duration aTime, Duration bTime)
        {
            throw new NotImplementedException();
        }

        public static Duration operator +(Duration aTime, Duration bTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     开始计时器
        /// </summary>
        public bool Start()
        {
            if (_RunFlag)
            {
                return false;
            }
            _RunFlag = true;
            Thread.Sleep(0);
            Begin = DateTime.Now;
            DurationMethods.QueryPerformanceCounter(out _StartTime);
            return true;
        }

        /// <summary>
        ///     停止计时器
        /// </summary>
        public bool Stop()
        {
            if (!_RunFlag)
            {
                return false;
            }
            _RunFlag = false;
            End = DateTime.Now;
            DurationMethods.QueryPerformanceCounter(out _StopTime);
            _DurationValue = (_StartTime - _StopTime)/(double) _Freq*1000;
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Begin:").Append(Begin.ToLongTimeString());
            sb.Append('|');
            sb.Append("End:").Append(End.ToLongTimeString());
            sb.Append('|');
            sb.Append("Duration:").Append(DurationValue.ToString());
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            var inputDur = (Duration) obj;
            if (!Begin.Equals(inputDur.Begin))
            {
                return false;
            }
            if (!End.Equals(inputDur.End))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return unchecked(27*Begin.GetHashCode() + End.GetHashCode() + DurationValue.GetHashCode());
        }

        internal static class DurationMethods
        {
            [DllImport("Kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

            [DllImport("Kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool QueryPerformanceFrequency(out long lpFrequency);
        }
    }
}