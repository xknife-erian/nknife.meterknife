using System.Xml;

namespace MeterKnife.Util.XML
{
    /// <summary>
    /// 对.net的Xml常使用的XmlNode, XmlDocument, XmlElement类的基类的封装
    /// </summary>
    public abstract class AbstractBaseXmlNode
    {
        /// <summary>
        /// 内部的XmlNode（组合）
        /// </summary>
        internal protected XmlNode BaseXmlNode;

        public virtual void AppendChild(AbstractBaseXmlNode baseNode)
        {
            this.BaseXmlNode.AppendChild(baseNode.BaseXmlNode);
        }

        public virtual void RemoveChild(AbstractBaseXmlNode baseNode)
        {
            this.BaseXmlNode.RemoveChild(baseNode.BaseXmlNode);
        }

        public virtual string GetAttribute(string name)
        {
            return (this.BaseXmlNode as XmlElement).GetAttribute(name);
        }

        public virtual void SetAttribute(string name, string value)
        {
            (this.BaseXmlNode as XmlElement).SetAttribute(name, value);
        }
    }
}
