using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Common.Logging;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.Xml;
using NKnife.Utility;

namespace SocketKnife.Generic.Protocol.Xmls
{
    /// <summary>含有序列化对象的解析器
    /// </summary>
    public class XmlProtocolDeserializeUnPacker : XmlProtocolUnPacker
    {
        private static readonly ILog _logger = LogManager.GetLogger<XmlProtocolDeserializeUnPacker>();

        protected override void ParseTags(StringProtocol content, XmlElement tagsElement)
        {
            content.Tags = new List<object>();
            foreach (XmlNode node in tagsElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.CDATA)
                    continue;
                var itemElement = (XmlCDataSection) node;
                object obj = null;
                Type type = UtilityType.FindType(tagsElement.GetAttribute("type"));
                try
                {
                    var xs = new XmlSerializer(type);
                    using (var stream = new MemoryStream(Encoding.Default.GetBytes(itemElement.InnerText)))
                    {
                        obj = xs.Deserialize(stream);
                        content.Tags.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("反序列化协议Tag异常。{0}", ex.Message), ex);
                }
                content.Tags.Add(obj);
            }
        }
    }
}