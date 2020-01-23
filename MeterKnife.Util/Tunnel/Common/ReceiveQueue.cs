using System;
using NKnife.Collections;

namespace NKnife.MeterKnife.Util.Tunnel.Common
{
    public class ReceiveQueue : SyncQueue<Tuple<byte[], byte[]>>
    {
    }
}