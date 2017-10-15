using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base;
using MeterKnife.Base.Channels;
using MeterKnife.Interfaces;
using MeterKnife.Models;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Keysights
{
    public class KeysightAnswer : MeasureAnswer<string>
    {
        public KeysightAnswer(string jobNumber, KeysightChannel channel, Instrument instrument, ExhibitBase exhibit, string data) 
            : base(jobNumber, channel, instrument, exhibit, data)
        {
        }
    }
}
