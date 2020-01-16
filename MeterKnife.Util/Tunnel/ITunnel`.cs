using System;

namespace MeterKnife.Util.Tunnel
{
    public interface ITunnel : IDisposable
    {
        ITunnelConfig Config { get; set; }
        void AddFilters(params ITunnelFilter[] filter);
        void RemoveFilter(ITunnelFilter filter);
        void BindDataConnector(IDataConnector dataConnector);
    }
}