using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;

namespace MeterKnife.Instruments.Agilent
{
    public class Ag34401A : BaseMeter
    {
        public Ag34401A()
        {
            Parameters = new Ag34401AParameters();
        }
    }
}
