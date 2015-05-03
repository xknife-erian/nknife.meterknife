using System;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Base
{
    public abstract class BaseMeter : IMeter
    {
        public int Port { get; set; }
        public int GpibAddress { get; set; }
        public event EventHandler<CollectEventArgs> ReceviedCollectData;
        public event EventHandler<CollectEventArgs> ReceviedTemperatureData;
        public string Name { get; set; }
        public object Parameters { get; set; }

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