using System.Xml;
using MeterKnife.Instruments.Common;

namespace MeterKnife.Instruments.Specified.Agilent
{
    public sealed class Ag34401AParamPanel : ScpiParamPanel
    {
        public Ag34401AParamPanel(XmlElement element) 
            : base(element)
        {
        }

    }
}