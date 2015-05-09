using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;
using NKnife.Base;
using NKnife.Utility;

namespace MeterKnife.Common.Util
{
    public static class MeterUtil
    {
        public static Pair<string, string> SimplifyName(string fullname)
        {
            var nd = fullname.Split(new[] {','});
            if (UtilityCollection.IsNullOrEmpty(nd) || nd.Length < 2)
            {
                return Pair<string, string>.Build(fullname, fullname);
            }
            var pair = Pair<string, string>.Build(nd[0].Trim(), nd[1].Trim());
            return pair;
        }
    }
}
