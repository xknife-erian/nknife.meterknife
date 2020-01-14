using System;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Base
{
    public abstract class BaseAlgorithm : IElectronAlgorithm<double>
    {
        protected ushort _DecimalDigit = 8;

        /// <summary>
        /// 小数位数
        /// </summary>
        public virtual ushort DecimalDigit
        {
            get { return _DecimalDigit; }
            set { _DecimalDigit = value; }
        }

        /// <summary>
        /// 输出数据
        /// </summary>
        public double Output { get; protected set; }

        /// <summary>
        /// 清除数据
        /// </summary>
        public virtual void Clear()
        {
            Output = 0;
        }

        /// <summary>
        /// 输入数据
        /// </summary>
        /// <param name="src">指定的输入数据</param>
        public abstract void Input(double src);

        public override string ToString()
        {
            return Math.Round(Output, DecimalDigit).ToString();
        }
    }
}