using System;
using System.Diagnostics;

namespace NKnife.Protocol.Generic.TextPlain
{
    public class TextPlainUnPacker : StringProtocolUnPacker
    {
        public override void Execute(StringProtocol content, string data, string command)
        {
            content.Command = command;
            string[] array = data.Split(new[] { TextPlainProtocolFlags.SplitFlag }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length > 1)
            {
                content.CommandParam = array[1];
            }
            if (array.Length > 2)
            {
                for (int i = 2; i < array.Length; i++)
                {
                    var node = array[i];
                    if (!node.Contains(TextPlainProtocolFlags.InfomationSplitFlag))
                    {
                        content.AddTag(node);
                    }
                    else
                    {
                        var vam = node.Split(new[] { TextPlainProtocolFlags.InfomationSplitFlag }, StringSplitOptions.RemoveEmptyEntries);
                        if (vam.Length >= 2)
                        {
                            content.AddInfo(vam[0], vam[1]);
                        }
                        else
                        {
                            Debug.Fail("协议数据异常", node);
                        }
                    }
                }
            }
        }
    }
}