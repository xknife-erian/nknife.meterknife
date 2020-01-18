using System;
using NKnife.Util;

namespace MeterKnife.Util
{
    public class MeterUtil
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

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
            _Logger.Info($"仪器{namePair}无映射");
            return "ScpiMeter";
        }
    }
}
