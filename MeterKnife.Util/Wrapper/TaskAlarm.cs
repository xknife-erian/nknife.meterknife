using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace MeterKnife.Util.Wrapper
{
    /// <summary>
    /// 一个用来提醒任务时间节点的类型，象个小闹钟一样。
    /// </summary>
    public class TaskAlarm : IDisposable
    {
        readonly ManualResetEvent _ClockAliveEvent = new ManualResetEvent(false);
        
        private readonly bool _IsRunnig = false;
        private bool _IsTiming = false;
        private long _StartTime, _StopTime;
        private readonly long _Freq;
        private double _MilSecondCount = 0;

        /// <summary>
        /// Gets 任务持续时间
        /// </summary>
        /// <value>The duration.</value>
        public double Duration
        {
            get { return _Duration; }
        }
        private double _Duration = 0;

        public TaskAlarm()
        {
            if (AlarmEventMethods.QueryPerformanceFrequency(out _Freq) == false)
            {
                throw new Win32Exception();
            }
            _IsRunnig = true;
            var timingThread = new Thread(new ThreadStart(this.WaitAWaken));
            timingThread.IsBackground = true;
            timingThread.Start();
        }

        public void Dispose()
        {
            ResetAlarm();
        }

        /// <summary>
        /// 设置闹钟xx毫秒后发出提醒
        /// </summary>
        /// <param name="milSecondCount"></param>
        public void SetAlarmAfter(double milSecondCount)
        {
            if (milSecondCount <= 10)
            {
                throw new Exception("时长必须大于10，单位毫秒");
            }
            ResetAlarm();
            Start();
            //设置参数
            _MilSecondCount = milSecondCount;
            _IsTiming = true;
            //唤醒线程
            AWake();
        }

        /// <summary>
        /// 重置闹钟，状态回到初始
        /// </summary>
        public void ResetAlarm()
        {
            _IsTiming = false;
            _MilSecondCount = 0;
            _Duration = 0;
        }

        private void WaitAWaken()
        {
            while (_IsRunnig)
            {
                //阻塞线程
                Block();
                //开始计时
                StartTiming();
            }
        }

        private void AWake()
        {
            _ClockAliveEvent.Set();
        }

        private void Block()
        {
            _ClockAliveEvent.Reset();
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        private void StartTiming()
        {
            try
            {
                _ClockAliveEvent.WaitOne();
                while (_Duration < _MilSecondCount)
                {
                    Thread.Sleep(10);
                    Stop();
                }
                if (_IsTiming)
                    OnTimeoutAlarm(EventArgs.Empty);
            }
            catch
            {
                _IsTiming = false;
            }
        }

        /// <summary>
        /// 当时间到了的时候发生的事件
        /// </summary>
        public event EventHandler TimeoutAlarmEvent;
        /// <summary>
        /// 当时间到了的时候,Raises the <see cref="E:TimeoutAlarmEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnTimeoutAlarm(EventArgs e)
        {
            if (TimeoutAlarmEvent != null)
                TimeoutAlarmEvent(this, e);
        }

        /// <summary>
        /// 开始计时器
        /// </summary>
        private bool Start()
        {
            Thread.Sleep(0);
            AlarmEventMethods.QueryPerformanceCounter(out _StartTime);
            return true;
        }
        /// <summary>
        /// 停止计时器
        /// </summary>
        private bool Stop()
        {
            AlarmEventMethods.QueryPerformanceCounter(out _StopTime);
            this._Duration = (double)(_StopTime - _StartTime) / (double)_Freq * 1000;
            return true;
        }

        private static class AlarmEventMethods
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
