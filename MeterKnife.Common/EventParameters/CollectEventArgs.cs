using MeterKnife.Common.DataModels;
using NKnife.Events;

namespace MeterKnife.Common.EventParameters
{
    public class CollectEventArgs : EventArgs<CollectData>
    {
        public CollectEventArgs(string source, CollectData data)
            : base(data)
        {
            Source = source;
            CollectData = data;
        }

        public string Source { get; private set; }
        public CollectData CollectData { get; private set; }
    }
}
