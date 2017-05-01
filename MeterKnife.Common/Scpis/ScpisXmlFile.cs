using System.Xml;
using NKnife.XML;

namespace MeterKnife.Scpis
{
    public class ScpisXmlFile : AbstractXmlDocument
    {
        public ScpisXmlFile(string fullName) 
            : base(fullName)
        {
        }

        public override string RootNodeLocalName
        {
            get { return "instrument"; }
        }

        public XmlElement Groups()
        {
            var nodes = DocumentElement.SelectSingleNode("//scpigroups");
            return nodes as XmlElement;
        }
    }
}

