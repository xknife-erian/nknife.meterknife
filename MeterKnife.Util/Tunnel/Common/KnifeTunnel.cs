using MeterKnife.Util.Tunnel.Events;

namespace MeterKnife.Util.Tunnel.Common
{
    public class KnifeTunnel : ITunnel
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        protected IDataConnector _DataConnector;
        protected ITunnelFilterChain _FilterChain;
        private bool _IsDataConnectedBound;
        public ITunnelConfig Config { get; set; }

        public virtual void AddFilters(params ITunnelFilter[] filters)
        {
            if (_FilterChain == null)
            {
                SetFilterChain();
            }
            foreach (var filter in filters)
            {
                if (_FilterChain != null)
                    _FilterChain.AddLast(filter);
            }
        }

        public void RemoveFilter(ITunnelFilter filter)
        {
            _FilterChain.Remove(filter);
        }

        public void BindDataConnector(IDataConnector dataConnector)
        {
            if (!_IsDataConnectedBound)
            {
                _DataConnector = dataConnector;
                _DataConnector.SessionBuilt += OnSessionBuilt;
                _DataConnector.SessionBroken += OnSessionBroken;
                _DataConnector.DataReceived += OnDataReceived;
                foreach (var filter in _FilterChain)
                {
                    filter.SendToSession += OnFilterSendToSession;
                    filter.SendToAll += OnFilterSendToAll;
                    filter.KillSession += OnFilterKillSession;
                }
                _logger.Debug(string.Format("DataConnector[{0}]绑定成功", dataConnector.GetType()));
                _IsDataConnectedBound = true;
            }
            else
            {
                _logger.Debug(string.Format("DataConnector[{0}]已经绑定，不需重复绑定", dataConnector.GetType()));
            }
        }

        protected virtual void SetFilterChain()
        {
            _FilterChain = new TunnelFilterChain();
        }

        public virtual void Dispose()
        {
            _DataConnector.Stop();
        }

        private void OnFilterKillSession(object sender, SessionEventArgs e)
        {
            _DataConnector.KillSession(e.Item.Id);
        }

        private void OnFilterSendToAll(object sender, SessionEventArgs e)
        {
            //取得上一个（靠近dataconnector的）filter
            var currentFilter = sender as ITunnelFilter;
            if (currentFilter == null)
                return;

            var node = _FilterChain.Find(currentFilter);
            if (node == null)
                return;

            var previous = node.Previous;
            while (previous != null)
            {
                previous.Value.ProcessSendToAll(e.Item.Data);
                previous = previous.Previous;
            }

            _DataConnector.SendAll(e.Item.Data);
        }

        private void OnFilterSendToSession(object sender, SessionEventArgs e)
        {
            //取得上一个（靠近dataconnector的）filter
            var currentFilter = sender as ITunnelFilter;
            if (currentFilter == null)
            {
                return;
            }

            var node = _FilterChain.Find(currentFilter);
            if (node == null)
            {
                return;
            }

            var previous = node.Previous;
            while (previous != null)
            {
                previous.Value.ProcessSendToSession(e.Item);
                previous = previous.Previous;
            }

            _DataConnector.Send(e.Item.Id, e.Item.Data);
        }

        private void OnDataReceived(object sender, SessionEventArgs e)
        {
            foreach (var filter in _FilterChain)
            {
                var continueNextFilter = filter.PrcoessReceiveData(e.Item); // 调用filter对数据进行处理

                if (!continueNextFilter)
                    break;
            }
        }

        private void OnSessionBroken(object sender, SessionEventArgs e)
        {
            foreach (var filter in _FilterChain)
            {
                filter.ProcessSessionBroken(e.Item.Id);
            }
        }

        private void OnSessionBuilt(object sender, SessionEventArgs e)
        {
            foreach (var filter in _FilterChain)
            {
                filter.ProcessSessionBuilt(e.Item.Id);
            }
        }
    }
}