using System;

namespace SerialKnife.Pan.Common
{
    /// <summary>上位机向下位机发送的双向数据，没有收到回复
    /// </summary>
    public class NoResponseEventArgs : EventArgs
    {
        private readonly int _SenderId;

        /// <summary>消息Id
        /// </summary>
        /// <param name="senderId"></param>
        public NoResponseEventArgs(int senderId)
        {
            _SenderId = senderId;
        }

        public int SenderId
        {
            get { return _SenderId; }
        }
    }
}