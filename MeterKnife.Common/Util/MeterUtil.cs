using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;
using NKnife.Base;

namespace MeterKnife.Common.Util
{
    public static class MeterUtil
    {
        public static Pair<string, string> SimplifyName(string fullname)
        {
            var nd = fullname.Split(new[] {','});
            var pair = Pair<string, string>.Build(nd[0].Trim(), nd[1].Trim());
            return pair;
        }
    }
}
