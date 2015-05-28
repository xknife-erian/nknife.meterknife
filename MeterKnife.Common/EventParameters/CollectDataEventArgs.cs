using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using NKnife.Events;

namespace MeterKnife.Common.EventParameters
{
    public class CollectDataEventArgs : EventArgs<CollectData>
    {
        public CollectDataEventArgs(IMeter meter, CollectData data)
            : base(data)
        {
            Meter = meter;
            CollectData = data;
        }

        public IMeter Meter { get; private set; }
        public CollectData CollectData { get; private set; }
    }
}
