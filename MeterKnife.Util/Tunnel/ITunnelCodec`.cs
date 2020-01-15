using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    /// <summary>
    /// 协议的编解码器
    /// </summary>
    /// <typeparam name="TData">内容在传输过程所使用的数据形式</typeparam>
    public interface ITunnelCodec<TData>
    {
        /// <summary>
        /// 解码器
        /// </summary>
        IDatagramDecoder<TData> Decoder { get; set; }
        /// <summary>
        /// 编码器
        /// </summary>
        IDatagramEncoder<TData> Encoder { get; set; }
    }
}
