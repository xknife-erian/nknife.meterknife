using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace MeterKnife.Base.Channels
{
    public abstract class MeasureAnswer<T> : AnswerBase<T>
    {
        protected MeasureAnswer(string jobNumber, IChannel<T> channel, IId instrument, IId target, T data) 
            : base(channel, instrument, target, data)
        {
            JobNumber = jobNumber;
        }

        public string JobNumber { get; set; }
    }
}
