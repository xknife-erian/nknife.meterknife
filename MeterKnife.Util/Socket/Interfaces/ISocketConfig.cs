using NKnife.Tunnel;

namespace NKnife.Socket.Interfaces
{
    public interface ISocketConfig : ITunnelConfig
    {
        void Initialize(int receiveTimeout, int sendTimeout, int maxBufferSize, int maxConnectCount, int receiveBufferSize, int sendBufferSize);

        int ReceiveBufferSize { get; set; }
        int SendBufferSize { get; set; }

        /// <summary>
        ///     接收包大小
        /// </summary>
        int MaxBufferSize { get; set; }

        /// <summary>
        ///     最大用户连接数
        /// </summary>
        int MaxConnectCount { get; set; }

        /// <summary>
        ///     SOCKET 的 ReceiveTimeout属性
        /// </summary>
        int ReceiveTimeout { get; set; }

        /// <summary>
        ///     SOCKET 的 SendTimeout
        /// </summary>
        int SendTimeout { get; set; }
    }
}