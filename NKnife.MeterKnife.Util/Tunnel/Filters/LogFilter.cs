using System.Text;
using NKnife.MeterKnife.Util.Tunnel.Base;

namespace NKnife.MeterKnife.Util.Tunnel.Filters
{
    public class LogFilter : BaseTunnelFilter
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        public override bool ProcessReceiveData(ITunnelSession session)
        {
            _Logger.Debug($"收到数据，来自{session.Id}：{Encoding.Default.GetString(session.Data)}");
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
            _Logger.Debug($"发送数据，目标{session.Id}：{Encoding.Default.GetString(session.Data)}");
        }

        public override void ProcessSendToAll(byte[] data)
        {
            _Logger.Debug($"发送数据，目标全体Session：{Encoding.Default.GetString(data)}");
        }
    }
}