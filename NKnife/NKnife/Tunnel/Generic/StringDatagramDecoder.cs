using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel.Generic
{
    /// <summary>
    /// 解码器
    /// </summary>
    public abstract class StringDatagramDecoder : BaseDatagramDecoder<string>
    {
        /// <summary>
        /// 解码。将字节数组解析成字符串。
        /// </summary>
        /// <param name="data">需解码的字节数组.</param>
        /// <param name="finishedIndex">已完成解码的数组的长度.</param>
        /// <returns>字符串结果数组</returns>
        public abstract override string[] Execute(byte[] data, out int finishedIndex);
    }
}