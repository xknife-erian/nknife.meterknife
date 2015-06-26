using System.Xml;
using MeterKnife.Common.Controls;
using MeterKnife.Instruments.Common;

namespace MeterKnife.Instruments.Specified.Keithley
{
    public sealed class K2000ParamPanel : ScpiParamPanel
    {
        public K2000ParamPanel(XmlElement element) 
            : base(element)
        {
        }
    }
}