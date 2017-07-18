using System;
using MeterKnife.Events;

namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    ///     “测量”功能的基接口，描述“测量”功能的核心函数与事件。
    /// </summary>
    public interface IMeasureExtender : IExtender
    {
        /// <summary>
        ///     当采集到一个数据时
        /// </summary>
        event EventHandler<ExhibitDataCollectedEventArgs> AfterDataCollected;

        /// <summary>
        ///     当测量即将开始时
        /// </summary>
        event EventHandler<EventArgs> MeasureStarting;

        /// <summary>
        ///     测量启动
        /// </summary>
        void MeasureStart();

        /// <summary>
        ///     当测量开始后
        /// </summary>
        event EventHandler<EventArgs> MeasureStarted;

        /// <summary>
        ///     当测量即将暂停时
        /// </summary>
        event EventHandler<EventArgs> MeasurePauseing;

        /// <summary>
        ///     测量暂停
        /// </summary>
        void MeasurePause();

        /// <summary>
        ///     当测量暂停后
        /// </summary>
        event EventHandler<EventArgs> MeasurePauseed;

        /// <summary>
        ///     当测量即将停止时
        /// </summary>
        event EventHandler<EventArgs> MeasureStoping;

        /// <summary>
        ///     测量结束
        /// </summary>
        void MeasureStop();

        /// <summary>
        ///     当测量停止后
        /// </summary>
        event EventHandler<EventArgs> MeasureStoped;
    }
}