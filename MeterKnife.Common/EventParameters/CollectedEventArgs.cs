using System;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.EventParameters
{
    public class CollectedEventArgs : EventArgs
    {
        public CollectedEventArgs(CommPort carePort, int address, bool isCollected, string scpiGroupKey)
        {
            CarePort = carePort;
            GpibAddress = address;
            IsCollected = isCollected;
            ScpiGroupKey = scpiGroupKey;
        }

        public CommPort CarePort { get; private set; }
        public int GpibAddress { get; private set; }
        public string ScpiGroupKey { get; private set; }
        public bool IsCollected { get; private set; }
    }
}