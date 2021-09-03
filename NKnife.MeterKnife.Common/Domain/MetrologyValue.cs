using System;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    /// 标定值
    /// </summary>
    public struct MetrologyValue
    {
        /// <summary>
        /// 标定时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 标定值
        /// </summary>
        public double Value { get; set; }
    }
}