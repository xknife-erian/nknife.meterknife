using System;
using NKnife.Interface;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    /// <summary>
    ///     面向全局的测量数据广播服务。该测量服务以事件方式，广播测量指令所采集到的数据。
    /// </summary>
    public interface IAntCollectService : IEnvironmentItem
    {
        /// <summary>
        ///     当测量事物启动后采集到即时数据时发生。
        /// </summary>
        event EventHandler<CollectEventArgs> Collected;

        /// <summary>
        ///     当测量指令采集到数据时，将数据置入MeasureService服务中
        /// </summary>
        /// <param name="dut">指定的工程与被测物</param>
        /// <param name="data">数据</param>
        void AddValue((Engineering, DUT) dut, MeasureData data);
    }

    /// <summary>
    ///     当测量事物启动后采集到即时数据时发生的事件信息封装。
    /// </summary>
    public class CollectEventArgs : EventArgs
    {
        public CollectEventArgs((Engineering, DUT) dut, MeasureData measurements)
        {
            Time = DateTime.Now;
            DUT = dut;
            Measurements = measurements;
        }

        /// <summary>
        /// 数据分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 测量时的即时时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 被测物
        /// </summary>
        public (Engineering, DUT) DUT { get; set; }

        /// <summary>
        /// 测量值
        /// </summary>
        public MeasureData Measurements { get; set; }

    }
}