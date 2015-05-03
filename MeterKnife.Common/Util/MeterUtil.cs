using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Util
{
    public static class MeterUtil
    {
        public static string SimplifyName(string fullname)
        {
            var nd = fullname.Split(new[] {','});
            return nd[1].Trim();
        }
    }
}
