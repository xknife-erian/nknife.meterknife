using MeterKnife.Common.Base;
using MeterKnife.Instruments.Agilent;

namespace MeterKnife.Instruments.Keithley
{
    public class K2000 : BaseMeter
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