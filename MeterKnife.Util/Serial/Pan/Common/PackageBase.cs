using System;
using Common.Logging;

namespace MeterKnife.Util.Serial.Pan.Common
{
    /// <summary>数据包的基类，包含指令及信息与事件的封装
    /// </summary>
    public abstract class PackageBase
    {
        private static readonly ILog _logger = LogManager.GetLogger<PackageBase>();

        protected PackageBase(ushort port, byte[] dataToSend, SendInterval sendInterval)
        {
            Port = port;
            DataToSend = dataToSend;
            SendInterval = sendInterval;
        }

        /// <summary>用序号表示一个串口
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public ushort Port { get; set; }
        /// <summary>发送数据的间隔
        /// </summary>
        /// <value>
        /// The send interval.
        /// </value>
        public SendInterval SendInterval { get; set; }
        /// <summary>将要被发送的数据
        /// </summary>
        /// <value>
        /// The data to send.
        /// </value>
        public byte[] DataToSend { get; set; }

        /// <summary>当数据包发送到达后（可能有返回，也可能无返回）
        /// </summary>
        public virtual event EventHandler<PackageSentEventArgs> PackageSent;

        internal void OnPackageSent(PackageSentEventArgs e)
        {
            var handler = PackageSent;
            if (handler == null) return;
            try
            {
                handler.Invoke(null, e);
            }
            catch (Exception ex)
            {
                _logger.Warn("OnPackageSent:", ex);
            }
        }
    }
}