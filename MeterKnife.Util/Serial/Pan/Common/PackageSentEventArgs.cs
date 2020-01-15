using System;

namespace SerialKnife.Pan.Common
{
    /// <summary> 数据包发送完成事件参数
    /// </summary>
    public class PackageSentEventArgs : EventArgs
    {
        public PackageSentEventArgs(ushort port, bool replied, byte[] recv, object packageId = null)
        {
            Port = port;
            Replied = replied;
            ReceivedData = recv;
            PackageId = packageId;
        }

        /// <summary>发送有返回
        /// </summary>
        public bool Replied { private set; get; }

        /// <summary>返回的数据
        /// </summary>
        public byte[] ReceivedData { private set; get; }

        /// <summary>当有返回事件时需配对时可使用此ID进行匹配
        /// </summary>
        public object PackageId { get; private set; }

        /// <summary>数据发送串口
        /// </summary>
        public ushort Port { private set; get; }
    }
}