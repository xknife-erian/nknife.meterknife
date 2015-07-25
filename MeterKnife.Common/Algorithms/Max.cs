using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    public class Max : BaseAlgorithm 
    {
        public Max()
        {
            Output = -double.MaxValue;
        }

        public override void Input(double src)
        {
            if (src > Output)
                Output = src;
        }

        public override string ToString()
        {
            if (Output == -double.MaxValue)
                return "0";
            return base.ToString();
        }

    }
}