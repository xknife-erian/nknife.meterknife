using System;
using MeterKnife.Util.Tunnel;
using MeterKnife.Util.Tunnel.Base;

namespace MeterKnife.Util.Serial.Generic.Filters
{
    public class SerialLogFilter : BaseTunnelFilter
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        public override bool ProcessReceiveData(ITunnelSession session)
        {
            _Logger.Debug($"收到数据，来自{session.Id}：{session.Data.ToHexString()}");
            return true;
        }

        public override void ProcessSessionBroken(long id)
        {
            _Logger.Debug($"连接断开，来自{id}");
        }

        public override void ProcessSessionBuilt(long id)
        {
            _Logger.Debug($"连接建立,来自{id}");
        }

        public override void ProcessSendToSession(ITunnelSession session)
        {
            _Logger.Debug($"发送数据，目标{session.Id}：{session.Data.ToHexString()}");
        }
    }
}