using System;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;
using NKnife.Tunnel.Base;

namespace MeterKnife.Common.Base
{
    public abstract class BaseCareCommunicationService : ITunnelService<byte[]>
    {
        protected BaseCareCommunicationService()
        {
            IsInitialized = false;
            ScpiCommandQueue = new ScpiCommandQueue();
        }

        public bool IsInitialized { get; protected set; }

        public int Order
        {
            get { return 100; }
        }

        public string Description
        {
            get { return "Care通讯服务"; }
        }

        /// <summary>
        ///     是连接Care的串口
        /// </summary>
        public List<CarePort> Cares { get; protected set; }

        void ITunnelService<byte[]>.Bind(CarePort carePort, params BaseProtocolHandler<byte[]>[] handlers)
        {
            Bind(carePort, handlers.Cast<CareOneProtocolHandler>().ToArray());
        }

        public ScpiCommandQueue ScpiCommandQueue { get; set; }

        public abstract void Destroy();
        public abstract void Send(CarePort carePort, byte[] data);

        public bool StartService()
        {
            IsInitialized = Initialize();
            return IsInitialized;
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

        public abstract bool Start(CarePort carePort);
        public abstract bool Stop(CarePort carePort);
        public abstract bool Initialize();

        public event EventHandler<EventArgs<CarePort>> SerialInitialized;

        protected virtual void OnSerialInitialized(EventArgs<CarePort> e)
        {
            EventHandler<EventArgs<CarePort>> handler = SerialInitialized;
            if (handler != null)
                handler(this, e);
        }

        public abstract void Bind(CarePort carePort, params CareOneProtocolHandler[] handlers);

        public abstract void Remove(CarePort carePort, CareOneProtocolHandler handler);
    }
}