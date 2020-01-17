using System;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Util.Protocol;
using NKnife.Util;
using NLog;

namespace MeterKnife.Util.Tunnel.Base
{

    /// <summary>
    /// 具备数据T到Protocol处理能力的类
    /// 1、能够对T进行拼包操作
    /// </summary>
    public abstract class KnifeProtocolProcessorBase<TData>// : IProtocolProcessor<TOriginal>
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        protected ITunnelCodec<TData> _Codec;
        protected IProtocolFamily<TData> _Family;

        public virtual void Bind(ITunnelCodec<TData> codec, IProtocolFamily<TData> protocolFamily)
        {
            _Codec = codec;
            _logger.Info($"绑定Codec成功。{_Codec.Decoder.GetType().Name},{_Codec.Encoder.GetType().Name}");

            _Family = protocolFamily;
            _logger.Info($"协议族[{_Family.FamilyName}]绑定成功。");
        }

        /// <summary>
        /// 数据包处理。主要处理较异常的情况下的，半包的接包，粘包等现象
        /// </summary>
        /// <param name="dataPacket">当前新的数据包</param>
        /// <param name="unFinished">未完成处理的数据</param>
        /// <returns>未处理完成,待下个数据包到达时将要继续处理的数据(半包)</returns>
        public virtual IEnumerable<IProtocol<TData>> ProcessDataPacket(byte[] dataPacket, byte[] unFinished)
        {
            if (!UtilCollection.IsNullOrEmpty(unFinished))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = dataPacket.Length;
                dataPacket = unFinished.Concat(dataPacket).ToArray();
                _logger.Trace($"接包操作:半包:{unFinished.Length},原始包:{srcLen},接包后:{dataPacket.Length}");
            }

            int done;
            TData[] datagram = _Codec.Decoder.Execute(dataPacket, out done);

            IEnumerable<IProtocol<TData>> protocols = null;

            if (UtilCollection.IsNullOrEmpty(datagram))
            {
                _logger.Debug("协议消息无内容。");
            }
            else
            {
                protocols = ParseProtocols(datagram);
            }

            if (dataPacket.Length > done)
            {
                // 暂存半包数据，留待下条队列数据接包使用
                unFinished = new byte[dataPacket.Length - done];
                Buffer.BlockCopy(dataPacket, done, unFinished, 0, unFinished.Length);
                _logger.Trace($"半包数据暂存,数据长度:{unFinished.Length}");
            }

            return protocols;
        }
        protected virtual IEnumerable<IProtocol<TData>> ParseProtocols(TData[] datagram)
        {
            var protocols = new List<IProtocol<TData>>(datagram.Length);
            foreach (TData dg in datagram)
            {
                //if (string.IsNullOrWhiteSpace(dg)) 
                //    continue;
                TData command;
                try
                {
                    command = _Family.CommandParser.GetCommand(dg);
                }
                catch (Exception e)
                {
                    _logger.Error($"命令字解析异常:{e.Message},Data:{dg}");
                    continue;
                }
                _logger.Trace($"开始协议解析::命令字:{command},数据包:{dg}");

                IProtocol<TData> protocol;
                try
                {
                    protocol = _Family.Parse(command, dg);
                }
                catch (ArgumentNullException ex)
                {
                    _logger.Warn($"协议分装异常。内容:{dg};命令字:{command}。{ex.Message}", ex);
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"协议分装异常。{ex.Message}");
                    continue;
                }
                protocols.Add(protocol);
            }
            return protocols;
        }
    }
}
