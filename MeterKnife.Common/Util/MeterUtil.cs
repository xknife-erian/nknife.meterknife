using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using MeterKnife.Common.Interfaces;
using NKnife.Base;
using NKnife.Util;

namespace MeterKnife.Common.Util
{
    public class MeterUtil
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeterUtil>();

        public static Tuple<string, string> SimplifyName(string fullname)
        {
            var nd = fullname.Split(new[] {','});
            if (UtilCollection.IsNullOrEmpty(nd) || nd.Length < 2)
            {
                return new Tuple<string, string>(fullname, fullname);
            }
            var pair = new Tuple<string, string>(nd[0].Trim(), nd[1].Trim());
            return pair;
        }

        public static string Named(Tuple<string, string> namePair)
        {
            var name = namePair.Item1;
            if (name.Contains("344"))
                return namePair.Item2;
            if (name.Contains("2000"))
                return "K2000";
            _logger.Info($"仪器{namePair}无映射");
            return "ScpiMeter";
        }
    }
}
