using System;
using SocketKnife.Events;
using SocketKnife.Generic;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerFilter : ISocketFilter
    {
        Func<KnifeSocketSessionMap> SessionMapGetter { get; }

        void Bind(Func<KnifeSocketSessionMap> sessionMapGetter);

        /// <summary>
        ///     服务器侦听到新客户端连接事件
        /// </summary>
        event EventHandler<SocketSessionEventArgs> ClientCome;

        /// <summary>
        ///     连接出错或断开触发事件
        /// </summary>
        event EventHandler<ConnectionBrokenEventArgs> ClientBroken;
    }
}