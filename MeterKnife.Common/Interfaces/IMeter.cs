namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    /// 一个描述仪器的接口
    /// </summary>
    public interface IMeter
    {
        int GpibAddress { get; set; }
        string Name { get; set; }
        string SimpleName { get; }
        object Parameters { get; set; }
    }
}
