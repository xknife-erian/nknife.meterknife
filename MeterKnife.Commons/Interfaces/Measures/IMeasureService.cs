using System;
using System.Collections.Generic;
using MeterKnife.Models;
using NKnife.Interface;

namespace MeterKnife.Interfaces.Measures
{
    /// <summary>
    ///     面向全局的测量数据广播服务。该测量服务以事件方式，广播测量指令所采集到的数据。
    /// </summary>
    public interface IMeasureService : IEnvironmentItem
    {
        /// <summary>
        ///     被测量物的列表
        /// </summary>
        List<IExhibit> Exhibits { get; set; }

        /// <summary>
        ///     正在执行的测量工作列表
        /// </summary>
        List<MeasureJob> Jobs { get; set; }

        /// <summary>
        ///     当测量指令采集到数据时发生。
        /// </summary>
        event EventHandler<MeasureEventArgs> Measured;

        /// <summary>
        ///     当测量指令采集到数据时，将数据置入MeasureService服务中
        /// </summary>
        /// <param name="jobNumber">测量事件编号</param>
        /// <param name="exhibit">被测量物</param>
        /// <param name="value">测量数据</param>
        void AddValue(string jobNumber, IExhibit exhibit, double value);
    }
}