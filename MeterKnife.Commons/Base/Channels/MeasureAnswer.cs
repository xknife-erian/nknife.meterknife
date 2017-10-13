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
        protected MeasureAnswer(string jobId, IChannel<T> channel, IId device, IId target, T data) 
            : base(channel, device, target, data)
        {
            JobId = jobId;
        }

        public string JobId { get; set; }
    }
}
