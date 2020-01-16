using System;

namespace NKnife.Socket.Events
{
    /// <summary>
    ///     当Socket连接后事件发生时包含事件数据的类(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public class ConnectedEventArgs : EventArgs
    {
        public ConnectedEventArgs(bool isConnectedSucceed, string message)
        {
            IsConnectedSucceed = isConnectedSucceed;
            Message = message;
        }

        public string Message { get; private set; }
        public bool IsConnectedSucceed { get; set; }
    }
}