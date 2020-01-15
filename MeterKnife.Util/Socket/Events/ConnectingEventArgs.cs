using System;
using System.Net;

namespace SocketKnife.Events
{
    /// <summary>
    ///     当Socket即将启动连接包含事件数据的类
    /// </summary>
    public class ConnectingEventArgs : EventArgs
    {
        public ConnectingEventArgs(EndPoint serverInfo)
        {
            ServerInfo = serverInfo;
        }

        public EndPoint ServerInfo { get; set; }
    }
}