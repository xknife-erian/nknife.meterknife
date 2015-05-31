﻿using System;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;
using NKnife.Interface;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;

namespace MeterKnife.Common.Base
{
    public abstract class BaseCareCommunicationService : ITunnelService<byte[]>
    {
        protected BaseCareCommunicationService()
        {
            IsInitialized = false;
        }

        public bool IsInitialized { get; protected set; }

        public abstract Dictionary<int, CareOneProtocolHandler> CareHandlers { get; }

        public int Order
        {
            get { return 100; }
        }

        public bool StartService()
        {
            return Initialize();
        }

        public bool CloseService()
        {
            try
            {
                Destroy();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string Description
        {
            get { return "Care通讯服务"; }
        }

        void ITunnelService<byte[]>.Bind(int port, params BaseProtocolHandler<byte[]>[] handlers)
        {
            Bind(port, handlers.Cast<CareOneProtocolHandler>().ToArray());
        }

        public abstract void Destroy();
        public abstract bool Start(int port);
        public abstract bool Stop(int port);
        public abstract void Send(int port, byte[] data);
        public abstract bool Initialize();

        public event EventHandler<EventArgs<int>> SerialInitialized;

        protected virtual void OnSerialInitialized(EventArgs<int> e)
        {
            EventHandler<EventArgs<int>> handler = SerialInitialized;
            if (handler != null)
                handler(this, e);
        }

        public abstract void Bind(int port, params CareOneProtocolHandler[] handlers);

        public abstract void Remove(int port, CareOneProtocolHandler handler);
    }
}