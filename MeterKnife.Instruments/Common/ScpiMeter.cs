using MeterKnife.Common.Base;

namespace MeterKnife.Instruments.Common
{
    public class ScpiMeter : BaseMeter
    {
        public override BaseParamPanel ParamPanel
        {
            get
            {
                return new ScpiParamPanel(GetTempElement());
            }
        }
    }
}