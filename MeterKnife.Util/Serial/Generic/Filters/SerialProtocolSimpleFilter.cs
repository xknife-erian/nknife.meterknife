using System;
using System.Collections.Generic;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel;
using NKnife.Tunnel.Filters;

namespace NKnife.Serial.Generic.Filters
{
    /// <summary>
    /// 一个最简单的协议处理Filter,不进入Handler进行协议分发,直接抛出协议收到事件
    /// </summary>
    public class SerialProtocolSimpleFilter : BytesProtocolFilter
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialProtocolFilter>();
        private readonly byte[] _CurrentReceiveBuffer = new byte[1024];
        private int _CurrentReceiveByteLength;

        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        protected virtual void OnProtocolsReceived(EventArgs<IEnumerable<IProtocol<byte[]>>> e)
        {
            EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> handler = ProtocolsReceived;
            if (handler != null)
                handler(this, e);
        }

        public override bool PrcoessReceiveData(ITunnelSession session)
        {
            byte[] data = session.Data;
            int len = data.Length;
            if (len == 0)
            {
                return false;
            }

            if (_CurrentReceiveByteLength + len > 1024) //缓冲区溢出了，只保留后1024位
            {
                //暂时不做处理，直接抛弃
                _logger.Warn("收到的数据超出1024，缓冲区溢出，该条数据抛弃");
                return false;
            }

            var tempData = new byte[_CurrentReceiveByteLength + len];
            Array.Copy(_CurrentReceiveBuffer, 0, tempData, 0, _CurrentReceiveByteLength);
            Array.Copy(data, 0, tempData, _CurrentReceiveByteLength, data.Length);

            //交由父类的处理函数处理
            var unfinished = new byte[] {};
            IEnumerable<IProtocol<byte[]>> protocols = ProcessDataPacket(tempData, ref unfinished);

            //将未完成解析的数据暂存，待下次收到数据后进行处理
            if (unfinished.Length > 0)
            {
                Array.Copy(unfinished, 0, _CurrentReceiveBuffer, 0, unfinished.Length);
                _CurrentReceiveByteLength = unfinished.Length;
            }
            else
            {
                _CurrentReceiveByteLength = 0;
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