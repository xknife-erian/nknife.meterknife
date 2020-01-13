using System;

namespace NKnife.MeterKnife
{
    public class SlotWorkingEventArgs : EventArgs
    {
        public string SlotId { get; set; }
        public string UutId { get; set; }
    }
}