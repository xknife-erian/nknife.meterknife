using NKnife.Events;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common
{
    public class CollectDataEventArgs : EventArgs<MeasureData>
    {
        public CollectDataEventArgs(IMeter meter, MeasureData data)
            : base(data)
        {
            Meter = meter;
            CollectData = data;
        }

        public IMeter Meter { get; private set; }
        public MeasureData CollectData { get; private set; }
    }
}
