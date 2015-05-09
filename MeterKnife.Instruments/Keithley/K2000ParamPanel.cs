using System.Xml;

namespace MeterKnife.Instruments.Agilent
{
    public sealed class K2000ParamPanel : ScpiParamPanel
    {
        public K2000ParamPanel(XmlElement element)
        {
            ParseElement(element);
        }
    }
}