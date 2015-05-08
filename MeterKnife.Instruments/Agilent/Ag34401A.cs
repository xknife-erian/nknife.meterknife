using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Instruments.Properties;

namespace MeterKnife.Instruments.Agilent
{
    public class Ag34401A : BaseMeter
    {

        public override BaseParamPanel ParamPanel { get { return new Ag34401AParamPanel(GetElement()); } }

        private XmlDocument _Document;
        private XmlElement GetElement()
        {
            if (_Document == null)
            {
                var xml = Resources.CommandElement;
                _Document = new XmlDocument();
                _Document.LoadXml(xml);
            }
            return _Document.DocumentElement;
        }
    }
}
