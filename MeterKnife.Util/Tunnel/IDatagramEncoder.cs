namespace MeterKnife.Util.Tunnel
{
    /// <summary>
    /// 一个编码器接口。结果码是将被传输的数据类型，比如Socket与Serial的byte[]。
    /// </summary>
    /// <typeparam name="TData">内容在传输过程所使用的数据形式</typeparam>
    public interface IDatagramEncoder<in TData>
    {
        /// <summary>
        /// 执行编码
        /// </summary>
        /// <param name="data">需被编码的内容</param>
        /// <returns>结果码是将被传输的数据类型，比如Socket与Serial的byte[]。</returns>
        byte[] Execute(TData data);
    }
}
