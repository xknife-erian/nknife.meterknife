using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    public class Max : BaseAlgorithm 
    {
        public override void Input(double src)
        {
            if (src > Output)
                Output = src;
        }
    }
}