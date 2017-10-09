using System;
using MeterKnife.Base;
using MeterKnife.Scpis;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Interfaces.Measures
{
    public class MeasureEventArgs : EventArgs
    {
        public MeasureEventArgs(string jobNumber, ExhibitBase exhibit, double value, DateTime time)
        {
            JobNumber = jobNumber;
            Value = value;
            Exhibit = exhibit;
            Time = time;
        }

        /// <summary>
        /// 测量事件编号
        /// </summary>
        public string JobNumber { get; set; }
        /// <summary>
        /// 测量值
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 被测物
        /// </summary>
        public ExhibitBase Exhibit { get; set; }
        /// <summary>
        /// 测量时的即时时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}