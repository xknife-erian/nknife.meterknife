using System;
using NKnife.MeterKnife.Util.Tunnel.Events;

namespace NKnife.MeterKnife.Util.Tunnel
{
    public interface IDataConnector
    {
        bool Stop();
        bool Start();

        event EventHandler<SessionEventArgs> SessionBuilt;
        event EventHandler<SessionEventArgs> SessionBroken;
        event EventHandler<SessionEventArgs> DataReceived;

        void Send(long id, byte[] data, string relation);

        void SendAll(byte[] data, string relation);

        void KillSession(long id);
    }
}