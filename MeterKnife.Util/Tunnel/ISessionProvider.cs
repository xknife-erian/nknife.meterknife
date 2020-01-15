using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.UI;

namespace NKnife.Tunnel
{
    public interface ISessionProvider<TData, TSessionId>
    {
        void Send(TSessionId id, TData data);

        void SendAll(TData data);

        void KillSession(TSessionId id);
        bool SessionExist(TSessionId id);
    }
}
