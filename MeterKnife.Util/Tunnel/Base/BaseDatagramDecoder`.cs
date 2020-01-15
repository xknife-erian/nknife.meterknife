namespace NKnife.Tunnel.Base
{
    /// <summary>
    /// 解码器
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public abstract class BaseDatagramDecoder<TData> : IDatagramDecoder<TData>
    {
        public abstract TData[] Execute(byte[] data, out int finishedIndex);
    }
}