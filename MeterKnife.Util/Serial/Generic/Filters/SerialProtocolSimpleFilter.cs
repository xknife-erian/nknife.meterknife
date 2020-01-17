using System;
using System.Collections.Generic;
using MeterKnife.Util.Protocol;
using MeterKnife.Util.Tunnel;
using MeterKnife.Util.Tunnel.Filters;
using NKnife.Events;

namespace MeterKnife.Util.Serial.Generic.Filters
{
    /// <summary>
    /// 一个最简单的协议处理Filter,不进入Handler进行协议分发,直接抛出协议收到事件
    /// </summary>
    public class SerialProtocolSimpleFilter : BytesProtocolFilter
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly byte[] _currentReceiveBuffer = new byte[1024];
        private int _currentReceiveByteLength;

        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        protected virtual void OnProtocolsReceived(EventArgs<IEnumerable<IProtocol<byte[]>>> e)
        {
            ProtocolsReceived?.Invoke(this, e);
        }

        public override bool ProcessReceiveData(ITunnelSession session)
        {
            byte[] data = session.Data;
            int len = data.Length;
            if (len == 0)
            {
                return false;
            }

            if (_currentReceiveByteLength + len > 1024) //缓冲区溢出了，只保留后1024位
            {
                //暂时不做处理，直接抛弃
                _Logger.Warn("收到的数据超出1024，缓冲区溢出，该条数据抛弃");
                return false;
            }

            var tempData = new byte[_currentReceiveByteLength + len];
            Array.Copy(_currentReceiveBuffer, 0, tempData, 0, _currentReceiveByteLength);
            Array.Copy(data, 0, tempData, _currentReceiveByteLength, data.Length);

            //交由父类的处理函数处理
            var unfinished = new byte[] {};
            IEnumerable<IProtocol<byte[]>> protocols = ProcessDataPacket(tempData, ref unfinished);

            //将未完成解析的数据暂存，待下次收到数据后进行处理
            if (unfinished.Length > 0)
            {
                Array.Copy(unfinished, 0, _currentReceiveBuffer, 0, unfinished.Length);
                _currentReceiveByteLength = unfinished.Length;
            }
            else
            {
                _currentReceiveByteLength = 0;
            }

            if (protocols != null)
            {
                OnProtocolsReceived(new EventArgs<IEnumerable<IProtocol<byte[]>>>(protocols));
            }
            return true;
        }

        public override void ProcessSendToSession(ITunnelSession session)
        {
        }

        public override void ProcessSendToAll(byte[] data)
        {
        }

        public override void ProcessSessionBroken(long id)
        {
        }

        public override void ProcessSessionBuilt(long id)
        {
        }
    }
}