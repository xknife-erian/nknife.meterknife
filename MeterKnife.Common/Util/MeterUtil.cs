using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using MeterKnife.Common.Interfaces;
using NKnife.Base;
using NKnife.Utility;

namespace MeterKnife.Common.Util
{
    public class MeterUtil
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeterUtil>();

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

        public static string Named(Pair<string, string> namePair)
        {
            var name = namePair.Second;
            if (name.Contains("344"))
                return namePair.Second;
            if (name.Contains("2000"))
                return "K2000";
            _logger.Info(string.Format("仪器{0}无映射", namePair));
            return "ScpiMeter";
        }
    }
}
