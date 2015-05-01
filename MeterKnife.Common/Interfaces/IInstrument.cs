namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    /// 一个描述仪器的接口
    /// </summary>
    public interface IInstrument : ICollectSource
    {
        string Name { get; }
    }
}
