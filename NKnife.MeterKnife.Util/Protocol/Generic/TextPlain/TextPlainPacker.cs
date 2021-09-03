using System.Text;

namespace NKnife.MeterKnife.Util.Protocol.Generic.TextPlain
{
    public class TextPlainPacker : StringProtocolPacker
    {
        public override string Combine(StringProtocol protocol)
        {
            var command = string.Format("{0}{2}{1}{2}", protocol.Command, protocol.CommandParam, TextPlainProtocolFlags.SplitFlag);
            var sb = new StringBuilder(command);
            foreach (var tag in protocol.Tags)
            {
                sb.Append(tag).Append(TextPlainProtocolFlags.SplitFlag);
            }
            foreach (var info in protocol.Infomations)
            {
                sb.Append(info.Key)
                    .Append(TextPlainProtocolFlags.InfomationSplitFlag)
                    .Append(info.Value)
                    .Append(TextPlainProtocolFlags.SplitFlag);
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
