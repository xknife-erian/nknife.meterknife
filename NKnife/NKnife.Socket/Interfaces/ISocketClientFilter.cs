using System;
using NKnife.Events;
using SocketKnife.Common;
using SocketKnife.Events;

namespace SocketKnife.Interfaces
{
    public interface ISocketClientFilter : ISocketFilter
    {
        /// <summary>
        ///     即将连接事件
        /// </summary>
        event EventHandler<ConnectingEventArgs> Connecting;

        /// <summary>
        ///     连接成功后事件
        /// </summary>
        event EventHandler<ConnectedEventArgs> Connected;

        /// <summary>
        ///     当连接断开后发生的事件
        /// </summary>
        event EventHandler<ConnectionBrokenEventArgs> ConnectionBroken;
    }
}