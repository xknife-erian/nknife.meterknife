using System;
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
            throw new NotImplementedException();
        }

        public bool CloseService()
        {
            throw new NotImplementedException();
        }

        public int Order { get; } = 999;
        public string Description { get; } = "面向全局的测量数据广播服务。";

        #endregion

        #region Implementation of IMeasureService

        public event EventHandler<MeasureEventArgs> Measured;

        protected virtual void OnMeasured(MeasureEventArgs e)
        {
            Measured?.Invoke(this, e);
        }

        public void AddValue(ushort number, double value, IExhibit exhibit)
        {
            OnMeasured(new MeasureEventArgs(number, value, exhibit));
        }

        #endregion
    }
}