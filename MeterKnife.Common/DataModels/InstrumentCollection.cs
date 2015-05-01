using System.Collections.Generic;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.DataModels
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