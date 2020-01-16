using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Common.Logging;
using NKnife.Interface;
using NKnife.Util;

namespace NKnife.Protocol.Generic.Xml
{
    public class XmlProtocolUnPacker : StringProtocolUnPacker
    {
        private static readonly ILog _logger = LogManager.GetLogger<XmlProtocolUnPacker>();

        #region IProtocolParser Members

        public override void Execute(StringProtocol content, string data, string command)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return;
            }
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(data);
            }
            catch (Exception e)
            {
                _logger.Warn("非XML协议数据:" + data, e);
            }
            try
            {
                ParseParm(content, doc.DocumentElement);
                if (doc.DocumentElement != null)
                {
                    var ele = (XmlElement) doc.DocumentElement.SelectSingleNode(XmlProtocolNames.Infos);
                    if (ele != null)
                        ParseInfos(content, ele);
                    ele = (XmlElement) doc.DocumentElement.SelectSingleNode(XmlProtocolNames.Tags);
                    if (ele != null)
                        ParseTags(content, ele);
                }
            }
            catch (Exception e)
            {
                _logger.Warn("解析协议数据异常。", e);
            }
        }

        #endregion

        protected virtual void ParseInfos(StringProtocol content, XmlElement infoElement)
        {
            foreach (XmlNode node in infoElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    var ele = node as XmlElement;
                    if (ele != null)
                        content.Infomations.Add(ele.LocalName, ele.InnerText);
                }
            }
        }

        protected virtual void ParseParm(StringProtocol content, XmlElement docElement)
        {
            if (docElement.HasAttribute(XmlProtocolNames.Param))
            {
                content.CommandParam = docElement.GetAttribute(XmlProtocolNames.Param);
            }
        }

        protected virtual void ParseTags(StringProtocol content, XmlElement tagsElement)
        {
            content.Tags = new List<object>();
            foreach (XmlNode node in tagsElement.ChildNodes)
            {

                if ((node.FirstChild.NodeType != XmlNodeType.Element) && (node.FirstChild.NodeType != XmlNodeType.CDATA))
                    continue;

                var itemElement = (XmlElement)node;
                var typeName = itemElement.GetAttribute("type");
                if(string.IsNullOrEmpty(typeName))
                    typeName= itemElement.GetAttribute("class");
                Type type = UtilType.FindType(typeName);
                try
                {
                    if (node.FirstChild.NodeType == XmlNodeType.Element)
                    {
                        const BindingFlags BF = BindingFlags.CreateInstance |
                                                (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance));
                        object obj = Activator.CreateInstance(type, BF, null, null, null);
                        var xml = obj as IXml;
                        if (xml != null)
                        {
                            xml.Parse(itemElement);
                        }
 
                        content.Tags.Add(obj);
                    }
                    if (node.FirstChild.NodeType == XmlNodeType.CDATA)
                    {
                        object e = UtilSerialize.Deserialize(node.InnerText, type);
                        content.Tags.Add(e);
                    }
                }
                catch (Exception e)
                {
                    Debug.Fail("从Tag创建对象时异常", e.Message);
                }
            }
        }
    }
}