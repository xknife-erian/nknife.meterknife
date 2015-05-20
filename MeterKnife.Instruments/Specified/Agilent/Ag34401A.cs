using MeterKnife.Common.Base;

namespace MeterKnife.Instruments.Specified.Agilent
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
