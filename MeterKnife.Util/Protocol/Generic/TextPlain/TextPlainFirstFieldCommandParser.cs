using System;

namespace MeterKnife.Util.Protocol.Generic.TextPlain
{
    public class TextPlainFirstFieldCommandParser : StringProtocolCommandParser
    {
        public override string GetCommand(string datagram)
        {
            var index = datagram.IndexOf("|", StringComparison.Ordinal);
            if (index > 0)
                return datagram.Substring(0, index);
            return string.Empty;
        }
    }
}
