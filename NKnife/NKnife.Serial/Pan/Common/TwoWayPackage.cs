using System;

namespace SerialKnife.Pan.Common
{
    /// <summary>发送接收信息包（双向）
    /// </summary>
    public class TwoWayPackage : PackageBase
    {
        public TwoWayPackage(ushort port, byte[] dataToSend, SendInterval sendInterval, int sendTimes = 1,object packageId = null)
            : base(port, dataToSend, sendInterval)
        {
            SendTimes = sendTimes;
            PackageId = packageId;
        }

        /// <summary>当有返回事件时需配对时可使用此ID进行匹配
        /// </summary>
        public object PackageId { get; set; }

        /// <summary>已经发出的次数
        /// </summary>
        public int AlreadySentTimes { get; set; }

        /// <summary>重发计数
        /// </summary>
        public int SendTimes { get; set; }

        /// <summary>无返回事件
        /// </summary>
        public event EventHandler<NoResponseEventArgs> NoResponsed;

        internal void OnNoResponse()
        {
            //EventHandler<NoResponseEventArgs> handler = NoResponsed;
            //if (handler != null)
            //{
            //    //TODO:有问题
            //    var e = new NoResponseEventArgs(0);
            //    handler(this, e);
            //}
        }
    }
}