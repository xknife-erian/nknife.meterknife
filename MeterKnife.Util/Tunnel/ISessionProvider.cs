
namespace NKnife.MeterKnife.Util.Tunnel
{
    public interface ISessionProvider<TData, TSessionId>
    {
        void Send(TSessionId id, TData data);

        void SendAll(TData data);

        void KillSession(TSessionId id);
        bool SessionExist(TSessionId id);
    }
}
