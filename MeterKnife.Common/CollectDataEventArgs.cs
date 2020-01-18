using NKnife.Events;
using NKnife.MeterKnife.Common.DataModels;

namespace NKnife.MeterKnife.Common
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
