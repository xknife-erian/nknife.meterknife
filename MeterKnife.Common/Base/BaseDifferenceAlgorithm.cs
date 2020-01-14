using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Base
{
    public abstract class BaseDifferenceAlgorithm : BaseAlgorithm
    {
        /// <summary>
        /// 被比较的值
        /// </summary>
        public BaseAlgorithm ValueOfComparison { get; set; }
    }
}
