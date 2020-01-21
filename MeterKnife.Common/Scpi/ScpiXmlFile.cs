using System.Xml;
using NKnife.XML;

namespace NKnife.MeterKnife.Common.Scpi
{
    public class ScpiXmlFile : AbstractXmlDocument
    {
        public ScpiXmlFile(string fullName) 
            : base(fullName)
        {
        }

        public override string RootNodeLocalName => "instrument";

        public XmlElement Groups()
        {
            var nodes = DocumentElement.SelectSingleNode("//scpigroups");
            return nodes as XmlElement;
        }
    }
}

