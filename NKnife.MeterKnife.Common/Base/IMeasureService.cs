using System;
using NKnife.Interface;

namespace NKnife.MeterKnife.Common.Base
{
    /// <summary>
    ///     面向全局的测量数据广播服务。该测量服务以事件方式，广播测量指令所采集到的数据。
    /// </summary>
    public interface IMeasureService : IEnvironmentItem
    {
        // /// <summary>
        // /// 创建一个测量事务
        // /// </summary>
        // MeasureJob CreateMeasureJob();
        //
        // /// <summary>
        // ///     被测量物的列表
        // /// </summary>
        // ICollection<IExhibit> Exhibits { get; set; }
        //
        // /// <summary>
        // /// 当新增被测物后发生
        // /// </summary>
        // event EventHandler<EventArgs<IExhibit>> ExhibitAdded;
        //
        // /// <summary>
        // /// 当被测物移除后发生
        // /// </summary>
        // event EventHandler<EventArgs<IExhibit>> ExhibitRemoved;
        //
        // /// <summary>
        // ///     正在执行的测量工作列表
        // /// </summary>
        // ICollection<MeasureJob> Jobs { get; set; }
        //
        // /// <summary>
        // /// 当新增测量事物后发生
        // /// </summary>
        // event EventHandler<EventArgs<MeasureJob>> MeasureJobAdded;
        //
        // /// <summary>
        // /// 当测量事物移除后发生
        // /// </summary>
        // event EventHandler<EventArgs<MeasureJob>> MeasureJobRemoved;

        /// <summary>
        ///     当测量事物启动后采集到即时数据时发生。
        /// </summary>
        event EventHandler<MeasureEventArgs> Measured;

        /// <summary>
        ///     当测量指令采集到数据时，将数据置入MeasureService服务中
        /// </summary>
        /// <param name="jobNumber">测量事件编号</param>
        /// <param name="exhibitId">被测量物</param>
        /// <param name="value">测量数据</param>
        void AddValue(string jobNumber, string exhibitId, double value);

    }

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