using System;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Zip;

namespace NKnife.Tunnel.Generic
{
    /// <summary>
    ///     一个最常用的回复消息的字节数组生成器
    /// </summary>
    public class LengthHeadEncoder : StringDatagramEncoder
    {
        private static readonly ILog _logger = LogManager.GetLogger<LengthHeadEncoder>();

        public LengthHeadEncoder()
        {
            NeedReverse = false;
            EnabelCompress = false;
            Encoding = Encoding.UTF8;
        }

        #region IDatagramEncoder Members

        /// <summary>
        ///     长度头的数组是否需要反转
        /// </summary>
        public bool NeedReverse { get; set; }

        /// <summary>
        ///     是否启用Gzip压缩
        /// </summary>
        public bool EnabelCompress { get; set; }

        public Encoding Encoding { get; set; }

        public override byte[] Execute(string replay)
        {
            if (string.IsNullOrWhiteSpace(replay))
            {
                return null;
            }

            //处理是否压缩字符串的字节数组进行发送
            byte[] reBytes;
            if (EnabelCompress)
            {
                byte[] src = Encoding.GetBytes(replay);
                reBytes = CompressHelper.Compress(src);
                _logger.Trace(string.Format("发送字符串压缩:原始:{0},压缩后:{1}", src.Length, reBytes.Length));
            }
            else
            {
                reBytes = Encoding.GetBytes(replay);
            }
            //得到将要发送的数据的长度
            byte[] head = BitConverter.GetBytes(reBytes.Length);
            if (NeedReverse)
                Array.Reverse(head);

            return head.Concat(reBytes).ToArray();
        }

        #endregion
    }
}