using System.Xml;

namespace MeterKnife.Instruments.Keithley
{
    public sealed class K2000ParamPanel : ScpiParamPanel
    {
        public K2000ParamPanel(XmlElement element) 
            : base(element)
        {
        }
    }
}