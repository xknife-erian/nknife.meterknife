using System;
using MeterKnife.Models;

namespace MeterKnife.Interfaces.Gateways
{
    public class InstrumentAddedEventArgs : EventArgs
    {
        public Instrument Instrument { get; set; }

        public InstrumentAddedEventArgs(Instrument instrument)
        {
            Instrument = instrument;
        }
    }
}