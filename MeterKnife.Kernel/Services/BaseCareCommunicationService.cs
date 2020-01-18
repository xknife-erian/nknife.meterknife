using System;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Common;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Util.Scpi;
using MeterKnife.Util.Tunnel.Base;
using NKnife.Events;

namespace MeterKnife.Kernel.Services
{
    public abstract class BaseCareCommunicationService : ITunnelService<byte[]>
    {
        protected BaseCareCommunicationService()
        {
            IsInitialized = false;
        }

        public bool IsInitialized { get; protected set; }

        public int Order => 100;

        public string Description => "Care通讯服务";

        /// <summary>
        ///     是连接Care的串口
        /// </summary>
        public List<Slot> Cares { get; protected set; }

        void ITunnelService<byte[]>.Bind(Slot carePort, params BaseProtocolHandler<byte[]>[] handlers)
        {
            Bind(carePort, handlers.Cast<CareOneProtocolHandler>().ToArray());
        }

        //public ScpiQueue ScpiCommandQueue { get; set; }
        /// <summary>
        ///     销毁服务
        /// </summary>
        public abstract void Destroy();

        /// <summary>
        ///     向指定端口发送Scpi命令组
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="careItems">即将发送的命令组</param>
        public abstract void SendCommands(Slot carePort, params ScpiCommandQueue.Item[] careItems);

        /// <summary>
        ///     向指定端口发送将要循环使用的Scpi命令组
        /// </summary>
        /// <param name="carePort">指定端口</param>
        /// <param name="commandArrayKey">命令组的Key</param>
        /// <param name="careItems">即将发送的命令组</param>
        public abstract void SendLoopCommands(Slot carePort, string commandArrayKey, params ScpiCommandQueue.Item[] careItems);

        public abstract bool Start(Slot carePort);
        public abstract bool Stop(Slot carePort);

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

        public abstract bool Initialize();

        public event EventHandler<EventArgs<Slot>> SerialInitialized;

        public virtual void OnSerialInitialized(Slot carePort)
        {
            var handler = SerialInitialized;
            handler?.Invoke(this, new EventArgs<Slot>(carePort));
        }

        public abstract void Bind(Slot carePort, params CareOneProtocolHandler[] handlers);

        public abstract void Remove(Slot carePort, CareOneProtocolHandler handler);
    }
}