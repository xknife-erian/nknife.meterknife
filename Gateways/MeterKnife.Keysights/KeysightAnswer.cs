using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Keysights
{
    public class KeysightAnswer : AnswerBase<string>
    {
        public KeysightAnswer(IChannel<string> channel, IDevice device, IExhibit exhibit, string data) 
            : base(channel, device, exhibit, data)
        {
        }
    }
}
