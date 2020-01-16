namespace MeterKnife.Util.Interface
{
    /// <summary>
    /// 一个描述该类型能够生成指定字符串的接口。
    /// 一般都与IParse配对使用。
    /// </summary>
    public interface IGenerator
    {
        string Generator();
    }
}
