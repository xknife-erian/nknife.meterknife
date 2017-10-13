using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace MeterKnife.Base.Channels
{
    public abstract class MeasureQuestion<T> : QuestionBase<T>
    {
        protected MeasureQuestion(IChannel<T> channel, IId instrument, IId target, bool isLoop, T data)
            : base(channel, instrument, target, isLoop, data)
        {
        }
    }
}