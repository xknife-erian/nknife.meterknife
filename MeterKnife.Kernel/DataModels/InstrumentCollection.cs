using System.Collections.Generic;
using MonitorKnife.Common.Interfaces;

namespace MeterKnife.Kernel.DataModels
{
    public class InstrumentCollection
    {
        public Dictionary<int, IInstrument> _Instruments = new Dictionary<int, IInstrument>();

        public IInstrument this[int port, int address]
        {
            get { return _Instruments[port*10000 + address*100]; }
            set { _Instruments[port*10000 + address*100] = value; }
        }
    }
}