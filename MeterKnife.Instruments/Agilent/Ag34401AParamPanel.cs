using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Instruments.Agilent
{
    public sealed class Ag34401AParamPanel : ScpiParamPanel
    {
        public Ag34401AParamPanel(XmlElement element) 
        {
            ParseElement(element);
        }
    }
}