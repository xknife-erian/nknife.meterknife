using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel.Generic
{
    public abstract class BytesDatagramDecoder : BaseDatagramDecoder<byte[]>
    {
        /// <summary>
        /// 解码。将字节数组解析成字符串。
        /// </summary>
        /// <param name="data">需解码的字节数组.</param>
        /// <param name="finishedIndex">已完成解码的数组的长度.</param>
        /// <returns>字符串结果数组</returns>
        public abstract override byte[][] Execute(byte[] data, out int finishedIndex);
    }
}
