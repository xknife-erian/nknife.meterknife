using System;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface ITunnelFilter
    {
        #region 针对DataConnector的事件处理

        /// <summary>
        ///     tunnel接收到来自dataconnector数据时，遍历调用filter链表中所有filter的该方法，
        ///     filter在此方法中处理来自dataconnector的数据
        /// </summary>
        /// <param name="session">dataconnector会话数据包</param>
        /// <returns>
        ///     返回true，允许filter链表中下一个filter继续执行接收数据处理，
        ///     返回false则tunnel中断数据接收处理动作
        /// </returns>
        bool PrcoessReceiveData(ITunnelSession session);

        /// <summary>
        ///     tunnel接收到来自dataconnector的会话中断消息时，通过该方法通知filter链表中所有filter会话中断，
        ///     filter根据自身逻辑执行中断处理
        /// </summary>
        /// <param name="id">dataconnector会话id</param>
        void ProcessSessionBroken(long id);

        /// <summary>
        ///     tunnel接收到来自dataconnector的会话建立消息时，通过该方法通知filter链表中所有filter会话建立，
        ///     filter根据自身逻辑执行会话建立处理
        /// </summary>
        /// <param name="id"></param>
        void ProcessSessionBuilt(long id);

        #endregion

        #region 针对filter的事件处理和方法

        /// <summary>
        ///     filter的使用者（包括tunnel）
        ///     可以通过该方法由filter对要发送的数据进行处理（注意,filter是不能直接调用dataconnector发送数据的）,
        ///     filter处理完数据后，可以通过OnSendToSession事件将处理后的数据抛给tunnel,tunnel会从filter链表中
        ///     找到当前filter前一个filter继续执行本方法，如果当前filter已经是链表中的第一个filter，则tunnel会调用dataconnctor的数据发送方法，
        ///     将数据发出，从而实现任何一个filter要发出在数据也可以经过其filter链表之前的filter过滤或拦截（filter处理后觉得不需要继续发送，
        ///     OnSendToSession事件即可）
        /// </summary>
        /// <param name="session"></param>
        void ProcessSendToSession(ITunnelSession session);

        /// <summary>
        ///     filter的使用者（包括tunnel）
        ///     可以通过该方法由filter对要发送的数据进行处理（注意,filter是不能直接调用dataconnector发送数据的）,
        ///     filter处理完数据后，可以通过OnSendToAll事件将处理后的数据抛给tunnel,tunnel会从filter链表中
        ///     找到当前filter前一个filter继续执行本方法，如果当前filter已经是链表中的第一个filter，则tunnel会调用dataconnctor的数据发送方法，
        ///     将数据发出，从而实现任何一个filter要发出在数据也可以经过其filter链表之前的filter过滤或拦截（filter处理后觉得不需要继续发送，
        ///     OnSendToAll事件即可）
        /// </summary>
        /// <param name="data"></param>
        void ProcessSendToAll(byte[] data);

        /// <summary>
        ///     filter通过该消息，通知tunnel，有数据想通过dataconnctor发送给指定会话，tunnel会从filter链表中
        ///     找到当前filter前一个filter继续执行ProcessSendToSession方法，如果当前filter已经是链表中的第一个filter，
        ///     则tunnel会调用dataconnctor的数据发送方法，
        /// </summary>
        event EventHandler<SessionEventArgs> SendToSession;

        /// <summary>
        ///     filter通过该消息，通知tunnel，有数据想通过dataconnctor发送给所有会话，tunnel会从filter链表中
        ///     找到当前filter前一个filter继续执行ProcessSendToAll方法，如果当前filter已经是链表中的第一个filter，
        ///     则tunnel会调用dataconnctor的数据发送方法，
        /// </summary>
        event EventHandler<SessionEventArgs> SendToAll;

        /// <summary>
        ///     filter自身想主动中断会话，则通过该事件通知tunnel,tunnel收到该事件后会主动断开会话
        /// </summary>
        event EventHandler<SessionEventArgs> KillSession;

        #endregion
    }
}