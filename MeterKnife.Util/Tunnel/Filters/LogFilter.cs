using System.Text;
using NKnife.MeterKnife.Util.Tunnel.Base;

namespace NKnife.MeterKnife.Util.Tunnel.Filters
{
    public class LogFilter : BaseTunnelFilter
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public override bool ProcessReceiveData(ITunnelSession session)
        {
            _logger.Debug(string.Format("收到数据，来自{0}：{1}", session.Id, Encoding.Default.GetString(session.Data)));
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
            _logger.Debug(string.Format("发送数据，目标{0}：{1}", session.Id, Encoding.Default.GetString(session.Data)));
        }

        public override void ProcessSendToAll(byte[] data)
        {
            _logger.Debug(string.Format("发送数据，目标全体Session：{0}", Encoding.Default.GetString(data)));
        }
    }
}