using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class TunnelBase : ITunnel<EndPoint, Socket, string>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        protected ITunnelFilterChain<EndPoint, Socket> _FilterChain;
        protected KnifeStringCodec _Codec;
        protected StringProtocolFamily _Family;
        protected List<KnifeSocketProtocolHandler> _Handlers = new List<KnifeSocketProtocolHandler>();

        protected IPAddress _IpAddress;
        protected int _Port;

        public abstract KnifeSocketConfig Config { get; set; }
        public abstract void Dispose();
        protected abstract void SetFilterChain();

        void ITunnel<EndPoint, Socket, string>.Bind(ITunnelCodec<string> codec, IProtocolFamily<string> protocolFamily)
        {
            Bind((KnifeStringCodec) codec, (StringProtocolFamily) protocolFamily);
        }

        void ITunnel<EndPoint, Socket, string>.AddHandlers(params IProtocolHandler<EndPoint, Socket, string>[] handlers)
        {
            foreach (var handler in handlers)
            {
                AddHandlers((KnifeSocketProtocolHandler)handler);
            }
        }

        public void AddHandlers(params KnifeSocketProtocolHandler[] handlers)
        {
            _Handlers.AddRange(handlers);
            OnBound(handlers);
        }

        void ITunnel<EndPoint, Socket, string>.RemoveHandler(IProtocolHandler<EndPoint, Socket, string> handler)
        {
            RemoveHandler((KnifeSocketProtocolHandler) handler);
        }

        public void RemoveHandler(KnifeSocketProtocolHandler handler)
        {
            _Handlers.Remove(handler);
        }

        ITunnelConfig ITunnel<EndPoint, Socket, string>.Config
        {
            get { return Config; }
            set { Config = (KnifeSocketConfig)value; }
        }

        void ITunnel<EndPoint, Socket, string>.AddFilters(params ITunnelFilter<EndPoint, Socket>[] filters)
        {
            foreach (var filter in filters)
            {
                AddFilters((KnifeSocketFilter) filter);
            }
        }

        public void RemoveFilter(ITunnelFilter<EndPoint, Socket> filter)
        {
            _FilterChain.Remove(filter);
        }

        public abstract bool Start();
        public abstract bool ReStart();
        public abstract bool Stop();

        public virtual void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
        }

        public virtual void AddFilters(params KnifeSocketFilter[] filters)
        {
            foreach (var filter in filters)
            {
                filter.BindGetter(() => _Codec, () => _Handlers, () => _Family);

                if (_FilterChain == null)
                    SetFilterChain();
                if (_FilterChain != null)
                    _FilterChain.AddLast(filter);
            }
        }

        public virtual void Bind(KnifeStringCodec codec, StringProtocolFamily protocolFamily)
        {
            _Codec = codec;
            _logger.Info(string.Format("绑定Codec成功。{0},{1}", _Codec.StringDecoder.GetType().Name, _Codec.StringEncoder.GetType().Name));

            _Family = protocolFamily;
            _logger.Info(string.Format("协议族[{0}]绑定成功。", _Family.FamilyName));
        }

        protected abstract void OnBound(params KnifeSocketProtocolHandler[] handlers);
    }
}