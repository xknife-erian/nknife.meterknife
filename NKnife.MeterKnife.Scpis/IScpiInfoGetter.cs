using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Common.Scpi;

namespace NKnife.MeterKnife.Scpis
{
    public interface IScpiInfoGetter
    {
        IEnumerable<ScpiCommandSubjectList> GetScpiSubjectCollections();
        List<Tuple<string, string, string>> GetMeterInfoList();
    }
}
