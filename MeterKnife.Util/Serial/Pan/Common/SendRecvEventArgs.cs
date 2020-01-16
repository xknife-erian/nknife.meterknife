using System;

namespace MeterKnife.Util.Serial.Pan.Common
{
    /// <summary>双向数据包发送后，收到回复的事件参数
    /// </summary>
    public class SendRecvEventArgs : EventArgs
    {
        private readonly byte[] _RecvData;
        private readonly int _SenderId;

        public SendRecvEventArgs(int senderId, byte[] recv, int count)
        {
            _SenderId = senderId;
            _RecvData = new byte[count];
            Array.Copy(recv, _RecvData, count);
        }

        public int SenderId
        {
            get { return _SenderId; }
        }

        /// <summary>接收到的数据
        /// </summary>
        public byte[] RecvData
        {
            get { return _RecvData; }
        }
    }
}