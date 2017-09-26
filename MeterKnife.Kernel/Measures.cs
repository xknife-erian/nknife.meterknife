using System;
using System.Threading;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Scpis;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Kernel
{
    public class Measures : IMeasureService
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

        public event EventHandler<MeasureEventArgs> Measured;

        protected virtual void OnMeasured(object e)
        {
            Measured?.Invoke(this, (MeasureEventArgs)e);
        }

        public void AddValue(ushort number, IExhibit exhibit, double value)
        {
            ThreadPool.QueueUserWorkItem(OnMeasured, new MeasureEventArgs(number, value, exhibit));
        }

        #endregion
    }
}