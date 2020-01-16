using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using Common.Logging;
using MeterKnife.Util.Protocol.Generic;
using MeterKnife.Util.Protocol.Generic.Xml;

namespace MeterKnife.Util.Socket.Generic.Protocol.Xmls
{
    /// <summary>含有序列化对象(DataTable)的解析器
    /// </summary>
    public class XmlProtocolDataTableDeserializeUnPacker : XmlProtocolUnPacker
    {
        private static readonly ILog _logger = LogManager.GetLogger<XmlProtocolDataTableDeserializeUnPacker>();

        protected override void ParseTags(StringProtocol content, XmlElement tagsElement)
        {
            content.Tags = new List<object>();
            foreach (XmlNode node in tagsElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.CDATA)
                    continue;

                var dtCData = (XmlCDataSection) node;
                const string head = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n";
                var sr = new StringReader(head + dtCData.InnerText);

                var dt = new DataTable();
                try
                {
                    dt.ReadXml(sr);
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("协议Tag解析异常。{0}", ex.Message), ex);
                }
                content.Tags.Add(dt);
            }
        }
    }
}