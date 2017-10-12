using System;
using System.Collections.Generic;
using System.Threading;
using MeterKnife.Base;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Models;
using NKnife.Events;

namespace MeterKnife.Kernel.Measures
{
    /// <summary>
    /// 面向全局的测量数据广播服务。该测量服务以事件方式，广播测量指令所采集到的数据。
    /// </summary>
    public class MeasureService : IMeasureService
    {
        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        public bool CloseService()
        {
            return true;
        }

        public int Order { get; } = 999;
        public string Description { get; } = "面向全局的测量数据广播服务";

        #endregion

        #region Implementation of IMeasureService

        /// <summary>
        ///     被测量物的列表
        /// </summary>
        public List<IExhibit> Exhibits { get; set; } = new List<IExhibit>(1);

        public event EventHandler<EventArgs<IExhibit>> ExhibitAdded;
        public event EventHandler<EventArgs<IExhibit>> ExhibitRemoved;

        /// <summary>
        ///     正在执行的测量工作列表
        /// </summary>
        public List<MeasureJob> Jobs { get; set; } = new List<MeasureJob>(1);

        public event EventHandler<EventArgs<MeasureJob>> MeasureJobAdded;
        public event EventHandler<EventArgs<MeasureJob>> MeasureJobRemoved;

        /// <summary>
        ///     当测量指令采集到数据时发生。
        /// </summary>
        public event EventHandler<MeasureEventArgs> Measured;

        /// <summary>
        ///     当测量指令采集到数据时，将数据置入MeasureService服务中
        /// </summary>
        /// <param name="jobNumber">测量事件编号</param>
        /// <param name="exhibit">被测量物</param>
        /// <param name="value">测量数据</param>
        public void AddValue(string jobNumber, IExhibit exhibit, double value)
        {
            ThreadPool.QueueUserWorkItem(OnMeasured, new MeasureEventArgs(jobNumber, exhibit.Id, value, DateTime.Now));
        }

        protected virtual void OnMeasured(object e)
        {
            Measured?.Invoke(this, (MeasureEventArgs) e);
        }

        #endregion
    }
}