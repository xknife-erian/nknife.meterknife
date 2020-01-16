using System;
using MeterKnife.Util.Tunnel;
using MeterKnife.Util.Tunnel.Base;

namespace MeterKnife.Util.Serial.Generic.Filters
{
    public class SerialLogFilter : BaseTunnelFilter
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public override bool PrcoessReceiveData(ITunnelSession session)
        {
            _logger.Debug(string.Format("收到数据，来自{0}：{1}", session.Id, session.Data.ToHexString()));
            return true;
        }

        public override void ProcessSessionBroken(long id)
        {
            _logger.Debug(string.Format("连接断开，来自{0}", id));
        }

        public override void ProcessSessionBuilt(long id)
        {
            _logger.Debug(string.Format("连接建立,来自{0}", id));
        }

        public override void ProcessSendToSession(ITunnelSession session)
        {
            _logger.Debug(string.Format("发送数据，目标{0}：{1}", session.Id, session.Data.ToHexString()));
        }
    }
}