using System;

namespace MeterKnife.Common.EventParameters
{
    public class CollectedEventArgs : EventArgs
    {
        public CollectedEventArgs(int address, bool isCollected)
        {
            GpibAddress = address;
            IsCollected = isCollected;
        }

        public int GpibAddress { get; private set; }
        public bool IsCollected { get; private set; }
    }
}