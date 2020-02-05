using System.Collections.Generic;
using NKnife.MeterKnife.Util.Tunnel.Base;
using NKnife.Util;

namespace NKnife.MeterKnife.Util.Tunnel.Filters
{
    public abstract class BytesProtocolFilter : BaseProtocolFilter<byte[]>
    {
        protected override bool ContainsCommand(List<byte[]> list, byte[] command)
        {
            if (list == null || command == null) return false;
            if (CommandCompareFunc == null)
            {
                foreach (var bs in list)
                {
                    if (bs.Length != command.Length)
                        continue;
                    var i = 0;
                    for (i = 0; i < bs.Length; i++)
                    {
                        if (bs[i] != command[i])
                            break;
                    }

                    if (i == bs.Length)
                        return true;
                }

                return false;
            }
            return CommandCompareFunc.Invoke(list, command);
        }
    }
}
