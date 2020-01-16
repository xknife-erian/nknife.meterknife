using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Util.Protocol;
using MeterKnife.Util.Tunnel.Common;
using MeterKnife.Util.Tunnel.Events;

namespace MeterKnife.Util.Tunnel.Base
{
    public abstract class BaseProtocolHandler<TData> : ITunnelProtocolHandler<TData>
    {
        private static readonly ILog _logger = LogManager.GetLogger<BaseProtocolHandler<TData>>();

        #region Codec

        private ITunnelCodec<TData> _CodecBase;

        public ITunnelCodec<TData> Codec
        {
            get { return _CodecBase; }
            set { _CodecBase = value; }
        }

        ITunnelCodec<TData> ITunnelProtocolHandler<TData>.Codec
        {
            get { return _CodecBase; }
            set { _CodecBase = value; }
        }

        #endregion

        protected IProtocolFamily<TData> _Family;
        public abstract List<TData> Commands { get; set; }
        public abstract void Recevied(long sessionId, IProtocol<TData> protocol);
        public event EventHandler<SessionEventArgs> SendToSession;
        public event EventHandler<SessionEventArgs> SendToAll;

        public virtual void Bind(ITunnelCodec<TData> codec, IProtocolFamily<TData> protocolFamily)
        {
            _CodecBase = codec;
            _Family = protocolFamily;
        }

        /// <summary>
        ///     发送协议，帮助方法
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="protocol"></param>
        public virtual void WriteToSession(long sessionId, IProtocol<TData> protocol)
        {
            try
            {
                TData original = _Family.Generate(protocol);
                byte[] data = _CodecBase.Encoder.Execute(original);
                EventHandler<SessionEventArgs> handler = SendToSession;
                if (handler != null)
                {
                    var session = new TunnelSession {Id = sessionId, Data = data};
                    var e = new SessionEventArgs(session);
                    handler.Invoke(this, e);
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("发送protocol异常,{0}", ex));
            }
        }

        /// <summary>
        /// 发送原始数据，帮助方法
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="data"></param>
        public virtual void WriteToSession(long sessionId, byte[] data)
        {
            try
            {
                EventHandler<SessionEventArgs> handler = SendToSession;
                if (handler != null)
                {
                    var session = new TunnelSession { Id = sessionId, Data = data };
                    var e = new SessionEventArgs(session);
                    handler.Invoke(this, e);
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("发送data异常,{0}", ex));
            }
        }

        public virtual void WriteToAllSession(IProtocol<TData> protocol)
        {
            try
            {
                TData str = _Family.Generate(protocol);
                byte[] data = _CodecBase.Encoder.Execute(str);
                EventHandler<SessionEventArgs> handler = SendToAll;
                if (handler != null)
                {
                    var session = new TunnelSession { Data = data };
                    handler.Invoke(this, new SessionEventArgs(session)); // new EventArgs<byte[]>(data));
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("发送protocol异常,{0}", ex));
            }
        }

        public virtual void WriteToAllSession(byte[] data)
        {
            try
            {
                EventHandler<SessionEventArgs> handler = SendToAll;
                if (handler != null)
                {
                    var session = new TunnelSession { Data = data };
                    handler.Invoke(this, new SessionEventArgs(session)); // new EventArgs<byte[]>(data));
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("发送data异常,{0}", ex));
            }
        }
    }
}