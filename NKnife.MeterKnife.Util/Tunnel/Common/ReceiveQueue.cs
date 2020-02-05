using System;
using NKnife.Collections;

namespace NKnife.MeterKnife.Util.Tunnel.Common
{
    /// <summary>
    /// 回复协议队列，第一值是关联物，第二值是查询指令，第三值是回复指令
    /// </summary>
    public class ReceiveQueue : SyncQueue<(string, byte[], byte[])>
    {
    }
}