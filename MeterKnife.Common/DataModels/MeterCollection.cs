using System.Collections.Generic;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.DataModels
{
    public class MeterCollection
    {
        public Dictionary<int, IMeter> _Meters = new Dictionary<int, IMeter>();

        public IMeter this[int port, int address]
        {
            get { return _Meters[port*10000 + address*100]; }
            set { _Meters[port*10000 + address*100] = value; }
        }
    }
}