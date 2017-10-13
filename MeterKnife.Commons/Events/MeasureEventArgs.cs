using System;

namespace MeterKnife.Events
{
    /// <summary>
    ///     当测量事物启动后采集到即时数据时发生的事件信息封装。
    /// </summary>
    public class MeasureEventArgs : EventArgs
    {
        public MeasureEventArgs(string jobId, string exhibitId, double value, DateTime time)
        {
            JobId = jobId;
            Value = value;
            ExhibitId = exhibitId;
            Time = time;
        }

        /// <summary>
        /// 测量事件编号
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// 被测物
        /// </summary>
        public string ExhibitId { get; set; }

        /// <summary>
        /// 测量值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 测量时的即时时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}