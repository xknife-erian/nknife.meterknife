using System;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Logic
{
    /// <summary>
    ///     面向全局的数据采集广播服务。该服务以事件方式，广播采集指令所采集到的数据。
    /// </summary>
    public class AntCollectService : IAntCollectService
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
        public string Description { get; } = "面向全局的采集数据广播服务";

        #endregion

        #region Implementation of IAntCollectService

        /// <summary>
        ///     当采集指令采集到数据时发生。
        /// </summary>
        public event EventHandler<CollectEventArgs> Collected;

        /// <summary>
        ///     当测量指令采集到数据时，将数据置入<see cref="AntCollectService"/>服务中
        /// </summary>
        /// <param name="dut">指定的工程与被测物</param>
        /// <param name="data">数据</param>
        public void AddValue((Engineering, DUT) dut, MeasureData data)
        {
            Task.Factory.StartNew(OnCollected, new CollectEventArgs(dut, data));
        }

        protected virtual void OnCollected(object e)
        {
            Collected?.Invoke(this, (CollectEventArgs) e);
        }

        #endregion
    }
}