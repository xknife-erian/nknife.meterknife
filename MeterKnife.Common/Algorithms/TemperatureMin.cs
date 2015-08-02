using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeterKnife.Common.Algorithms
{
    public class TemperatureMin : Min
    {
        private bool _IsFirst = true;

        public override void Input(double src)
        {
            if (_IsFirst)
            {
                if (src == 0)
                    return;
                _IsFirst = false;
            }
            if (src < Output)
                Output = src;
        }
    }
}
