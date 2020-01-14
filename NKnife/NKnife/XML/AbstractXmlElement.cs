using System.Xml;

namespace NKnife.XML
{
    /// <summary>
    /// 对XmlElement的封装类
    /// </summary>
    public abstract class AbstractXmlElement : AbstractBaseXmlNode
    {
        /// <summary>
        /// 对XmlElement的封装类的构造函数
        /// </summary>
        /// <param name="doc">XmlDocument的封装类</param>
        /// <param name="localname">Element的Localname</param>
        public AbstractXmlElement(AbstractXmlDocument doc, string localname)
        {
            this.BaseXmlNode = (doc.BaseXmlNode as XmlDocument).CreateElement(localname);
        }
    }
}
