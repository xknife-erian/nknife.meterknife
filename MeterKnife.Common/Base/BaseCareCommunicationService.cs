using System;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Common.Util;
using NKnife.Events;
using NKnife.Scpi;
using NKnife.Tunnel.Base;

namespace MeterKnife.Common.Base
{
    public abstract class BaseCareCommunicationService : ITunnelService<byte[]>
    {
        protected BaseCareCommunicationService()
        {
            IsInitialized = false;
            //ScpiCommandQueue = new ScpiQueue();
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
        public List<CommPort> Cares { get; protected set; }

        void ITunnelService<byte[]>.Bind(CommPort carePort, params BaseProtocolHandler<byte[]>[] handlers)
        {
            Bind(carePort, handlers.Cast<CareOneProtocolHandler>().ToArray());
        }

        //public ScpiQueue ScpiCommandQueue { get; set; }
        /// <summary>
        /// 销毁服务
        /// </summary>
        public abstract void Destroy();

        /// <summary>
        ///     向指定端口发送Scpi命令组
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="careItems">即将发送的命令组</param>
        public abstract void SendCommands(CommPort carePort, params ScpiCommandQueue.Item[] careItems);

        /// <summary>
        ///     向指定端口发送将要循环使用的Scpi命令组
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="commandArrayKey">命令组的Key</param>
        /// <param name="careItems">即将发送的命令组</param>
        public abstract void SendLoopCommands(CommPort carePort, string commandArrayKey, params ScpiCommandQueue.Item[] careItems);

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
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public abstract bool Start(CommPort carePort);
        public abstract bool Stop(CommPort carePort);
        public abstract bool Initialize();

        public event EventHandler<EventArgs<CommPort>> SerialInitialized;

        public virtual void OnSerialInitialized(CommPort carePort)
        {
            EventHandler<EventArgs<CommPort>> handler = SerialInitialized;
            if (handler != null)
                handler(this, new EventArgs<CommPort>(carePort));
        }

        public abstract void Bind(CommPort carePort, params CareOneProtocolHandler[] handlers);

        public abstract void Remove(CommPort carePort, CareOneProtocolHandler handler);
    }
}