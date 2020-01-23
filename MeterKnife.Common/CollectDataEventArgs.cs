using NKnife.Events;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common
{
    public class CollectDataEventArgs : EventArgs<MetricalData>
    {
        public CollectDataEventArgs(IMeter meter, MetricalData data)
            : base(data)
        {
            Meter = meter;
            MetricalData = data;
        }

        public IMeter Meter { get; private set; }
        public MetricalData MetricalData { get; private set; }
    }
}
