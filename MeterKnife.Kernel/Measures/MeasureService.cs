using System;
using System.Collections.Generic;
using System.Threading;
using MeterKnife.Base;
using MeterKnife.Interfaces.Measures;
using NKnife.Channels.Interfaces.Channels;

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
        public List<ExhibitBase> Exhibits { get; set; }

        /// <summary>
        ///     当测量指令采集到数据时发生。
        /// </summary>
        public event EventHandler<MeasureEventArgs> Measured;

        /// <summary>
        ///     当测量指令采集到数据时，将数据置入MeasureService服务中
        /// </summary>
        /// <param name="exhibit">被测量物</param>
        /// <param name="value">测量数据</param>
        public void AddValue(ExhibitBase exhibit, double value)
        {
            ThreadPool.QueueUserWorkItem(OnMeasured, new MeasureEventArgs(exhibit, value));
        }

        protected virtual void OnMeasured(object e)
        {
            Measured?.Invoke(this, (MeasureEventArgs) e);
        }

        #endregion
    }
}