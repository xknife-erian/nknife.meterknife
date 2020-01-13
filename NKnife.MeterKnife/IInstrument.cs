using System;

namespace NKnife.MeterKnife
{
    /// <summary>
    /// 测量仪器
    /// </summary>
    public interface IInstrument : IRouteEnable, IEquatable<IInstrument>
    {
    }
}