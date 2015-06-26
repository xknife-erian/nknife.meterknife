using System;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Base
{
    public abstract class BaseAlgorithm : IElectronAlgorithm<double>
    {
        protected BaseAlgorithm()
        {
            DecimalDigit = 8;
        }

        public ushort DecimalDigit { get; set; }
        public double Output { get; protected set; }

        public virtual void Clear()
        {
            Output = 0;
        }

        public abstract void Input(double src);

        public override string ToString()
        {
            return Math.Round(Output, DecimalDigit).ToString();
        }
    }
}