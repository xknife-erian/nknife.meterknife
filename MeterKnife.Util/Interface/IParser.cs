namespace NKnife.Interface
{
    /// <summary>
    /// 一个描述解析器的接口，一般情况下，实现类通常做为某种解析器使用。
    /// </summary>
    /// <typeparam name="TSource">被解析数据源的类型</typeparam>
    /// <typeparam name="TResult">将要解析出来的数据的类型</typeparam>
    public interface IParser<in TSource, TResult>
    {
        /// <summary>
        /// 解析器的核心方法。对数据进行解析。
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="result">解析后的数据结果</param>
        /// <returns>是否解析成功</returns>
        bool TryParse(TSource source, out TResult result);
    }
}