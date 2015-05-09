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

        public override BaseParamPanel ParamPanel
        {
            get
            {
                return new Ag34401AParamPanel(GetTempElement());
            }
        }
    }
}
