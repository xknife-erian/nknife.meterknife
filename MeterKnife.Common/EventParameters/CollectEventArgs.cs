using MeterKnife.Common.DataModels;
using NKnife.Events;

namespace MeterKnife.Common.EventParameters
{
    public class CollectEventArgs : EventArgs<CollectData>
    {
        public CollectEventArgs(int source, CollectData data)
            : base(data)
        {
            Address = source;
            CollectData = data;
        }

        public int Address { get; private set; }
        public CollectData CollectData { get; private set; }
    }
}
