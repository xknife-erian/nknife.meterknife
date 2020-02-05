namespace NKnife.MeterKnife.Common.Scpi
{
    /// <summary>
    /// Scpi保存文件可能发生版本变化，定义本接口控制未来的变化
    /// </summary>
    public interface IScpiFileVersionProcessor
    {
        ScpiXmlFile Update(ScpiXmlFile scpiFile, bool isSave);
    }
}
