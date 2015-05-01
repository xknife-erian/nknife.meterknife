using System;
using MeterKnife.Kernel.EventParameters;
using MonitorKnife.Common.Interfaces;

namespace MeterKnife.Kernel
{
    public abstract class BaseInstrument : IInstrument
    {
        public int Port { get; set; }
        public int GpibAddress { get; set; }
        public event EventHandler<CollectEventArgs> ReceviedCollectData;
        public event EventHandler<CollectEventArgs> ReceviedTemperatureData;
        public string Name { get; set; }

        protected virtual void OnReceviedCollectData(CollectEventArgs e)
        {
            EventHandler<CollectEventArgs> handler = ReceviedCollectData;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnReceviedTemperatureData(CollectEventArgs e)
        {
            EventHandler<CollectEventArgs> handler = ReceviedTemperatureData;
            if (handler != null) handler(this, e);
        }
    }
}