using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.Util.Tunnel.Common;
using NKnife.MeterKnife.Util.Tunnel.Events;
using NLog;

namespace NKnife.MeterKnife.Common.Tunnels
{
    public class CareTunnel : ITunnel
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();
        protected IDataConnector _dataConnector;
        protected ITunnelFilterChain _filterChain = new TunnelFilterChain();
        private bool _isDataConnectedBound;
        public ITunnelConfig Config { get; set; }

        public virtual void AddFilters(params ITunnelFilter[] filters)
        {
            foreach (var filter in filters) 
                _filterChain?.AddLast(filter);
        }

        public void RemoveFilter(ITunnelFilter filter)
        {
            _filterChain.Remove(filter);
        }

        public void BindDataConnector(IDataConnector dataConnector)
        {
            if (!_isDataConnectedBound)
            {
                _dataConnector = dataConnector;
                _dataConnector.SessionBuilt += OnSessionBuilt;
                _dataConnector.SessionBroken += OnSessionBroken;
                _dataConnector.DataReceived += OnDataReceived;
                foreach (var filter in _filterChain)
                {
                    filter.SendToSession += OnFilterSendToSession;
                    filter.SendToAll += OnFilterSendToAll;
                    filter.KillSession += OnFilterKillSession;
                }

                _Logger.Debug($"DataConnector[{dataConnector.GetType()}]绑定成功");
                _isDataConnectedBound = true;
            }
            else
            {
                _Logger.Debug($"DataConnector[{dataConnector.GetType()}]已经绑定，不需重复绑定");
            }
        }

        public virtual void Dispose()
        {
            _dataConnector.Stop();
        }

        private void OnFilterKillSession(object sender, SessionEventArgs e)
        {
            _dataConnector.KillSession(e.Item.Id);
        }

        private void OnFilterSendToAll(object sender, SessionEventArgs e)
        {
            //取得上一个（靠近dataConnector的）filter
            var currentFilter = sender as ITunnelFilter;
            if (currentFilter == null)
                return;

            var node = _filterChain.Find(currentFilter);
            if (node == null)
                return;

            var previous = node.Previous;
            while (previous != null)
            {
                previous.Value.ProcessSendToAll(e.Item.Data);
                previous = previous.Previous;
            }

            _dataConnector.SendAll(e.Item.Data);
        }

        private void OnFilterSendToSession(object sender, SessionEventArgs e)
        {
            //取得上一个（靠近dataConnector的）filter
            var currentFilter = sender as ITunnelFilter;
            if (currentFilter == null) return;

            var node = _filterChain.Find(currentFilter);
            if (node == null) return;

            var previous = node.Previous;
            while (previous != null)
            {
                previous.Value.ProcessSendToSession(e.Item);
                previous = previous.Previous;
            }

            _dataConnector.Send(e.Item.Id, e.Item.Data);
        }

        private void OnDataReceived(object sender, SessionEventArgs e)
        {
            foreach (var filter in _filterChain)
            {
                var continueNextFilter = filter.ProcessReceiveData(e.Item); // 调用filter对数据进行处理
                if (!continueNextFilter)
                    break;
            }
        }

        private void OnSessionBroken(object sender, SessionEventArgs e)
        {
            foreach (var filter in _filterChain) filter.ProcessSessionBroken(e.Item.Id);
        }

        private void OnSessionBuilt(object sender, SessionEventArgs e)
        {
            foreach (var filter in _filterChain) filter.ProcessSessionBuilt(e.Item.Id);
        }
    }
}