using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using NKnife.Protocol;
using NKnife.Utility;

namespace NKnife.Tunnel.Base
{

    /// <summary>
    /// 具备数据T到Protocol处理能力的类
    /// 1、能够对T进行拼包操作
    /// </summary>
    public abstract class KnifeProtocolProcessorBase<TData>// : IProtocolProcessor<TOriginal>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KnifeProtocolProcessorBase<TData>>();

        protected ITunnelCodec<TData> _Codec;
        protected IProtocolFamily<TData> _Family;

        public virtual void Bind(ITunnelCodec<TData> codec, IProtocolFamily<TData> protocolFamily)
        {
            _Codec = codec;
            _logger.Info(string.Format("绑定Codec成功。{0},{1}", _Codec.Decoder.GetType().Name, _Codec.Encoder.GetType().Name));

            _Family = protocolFamily;
            _logger.Info(string.Format("协议族[{0}]绑定成功。", _Family.FamilyName));
        }

        /// <summary>
        /// 数据包处理。主要处理较异常的情况下的，半包的接包，粘包等现象
        /// </summary>
        /// <param name="dataPacket">当前新的数据包</param>
        /// <param name="unFinished">未完成处理的数据</param>
        /// <returns>未处理完成,待下个数据包到达时将要继续处理的数据(半包)</returns>
        public virtual IEnumerable<IProtocol<TData>> ProcessDataPacket(byte[] dataPacket, byte[] unFinished)
        {
            if (!UtilityCollection.IsNullOrEmpty(unFinished))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = dataPacket.Length;
                dataPacket = unFinished.Concat(dataPacket).ToArray();
                _logger.Trace(string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", unFinished.Length, srcLen, dataPacket.Length));
            }

            int done;
            TData[] datagram = _Codec.Decoder.Execute(dataPacket, out done);

            IEnumerable<IProtocol<TData>> protocols = null;

            if (UtilityCollection.IsNullOrEmpty(datagram))
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
                _logger.Trace(string.Format("半包数据暂存,数据长度:{0}", unFinished.Length));
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
                    _logger.Error(string.Format("命令字解析异常:{0},Data:{1}", e.Message, dg), e);
                    continue;
                }
                _logger.Trace(string.Format("开始协议解析::命令字:{0},数据包:{1}", command, dg));

                IProtocol<TData> protocol;
                try
                {
                    protocol = _Family.Parse(command, dg);
                }
                catch (ArgumentNullException ex)
                {
                    _logger.Warn(string.Format("协议分装异常。内容:{0};命令字:{1}。{2}", dg, command, ex.Message), ex);
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("协议分装异常。{0}", ex.Message), ex);
                    continue;
                }
                protocols.Add(protocol);
            }
            return protocols;
        }
    }
}
