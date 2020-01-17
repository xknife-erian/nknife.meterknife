namespace MeterKnife.Util.Protocol
{
    /// <summary>
    ///     一个协议的具体内容。其中：
    ///     Infomations,Tags均属于协议的内容
    ///     Infomations:固定数据，按协议规定的必须每次携带的数据
    ///     Tags:内容较大的数据,一般为可序列化的对象
    /// </summary>
    public interface IProtocolContent<T>
    {
        /// <summary>
        ///     协议命令字
        /// </summary>
        T Command { get; set; }


    }
}