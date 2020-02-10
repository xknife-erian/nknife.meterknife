using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using NKnife.Events;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Logic
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
        ///     当测量指令采集到数据时发生。
        /// </summary>
        public event EventHandler<MeasureEventArgs> Measured;

        /// <summary>
        ///     当测量指令采集到数据时，将数据置入MeasureService服务中
        /// </summary>
        /// <param name="dut">指定的工程与被测物</param>
        /// <param name="data">数据</param>
        public void AddValue((Engineering, DUT) dut, MeasureData data)
        {
            Task.Factory.StartNew(OnMeasured, new MeasureEventArgs(DateTime.Now, dut, data));
        }

        protected virtual void OnMeasured(object e)
        {
            Measured?.Invoke(this, (MeasureEventArgs) e);
        }

        #endregion

    }
}