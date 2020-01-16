namespace MeterKnife.Util.Interface
{
    /// <summary>
    /// 一个描述能够针对字符串解析类型的接口。
    /// 一般都与IGenerator配对使用。
    /// </summary>
    public interface IParse
    {
        void Parse(string value);
    }
}
