using System.ComponentModel;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Base
{
    public class DMMParameters : IMeterParameters
    {
        public DMMParameters()
        {
            NPLC = 10;
        }

        [Category("基本"), DisplayName("测量")]
        public DMMMeasure DMMMeasure { get; set; }

        [Category("基本"), DisplayName("范围")]
        public DMMRange Range { get; set; }

        [Category("基本"), DisplayName("自动调零")]
        public DMMZeroSet DMMZeroSet { get; set; }

        [Category("积分时间"), DisplayName("NPLC")]
        public ushort NPLC { get; set; }

        [Category("积分时间"), DisplayName("分辩率")]
        public DMMRate Rate { get; set; }

    }
}