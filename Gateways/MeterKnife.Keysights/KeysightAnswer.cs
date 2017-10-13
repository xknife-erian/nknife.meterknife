using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base;
using MeterKnife.Interfaces;
using MeterKnife.Models;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Keysights
{
    public class KeysightAnswer : AnswerBase<string>
    {
        public KeysightAnswer(KeysightChannel channel, Instrument device, ExhibitBase exhibit, string data) 
            : base(channel, device, exhibit, data)
        {
        }

        public string JobNumber { get; }
    }
}
