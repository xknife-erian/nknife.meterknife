using System;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.EventParameters
{
    public class CollectedEventArgs : EventArgs
    {
        public CollectedEventArgs(CarePort carePort, int address, bool isCollected)
        {
            CarePort = carePort;
            GpibAddress = address;
            IsCollected = isCollected;
        }

        public CarePort CarePort { get; private set; }
        public int GpibAddress { get; private set; }
        public bool IsCollected { get; private set; }
    }
}