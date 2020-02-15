using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Common.Scpi;

namespace NKnife.MeterKnife.Scpis
{
    public interface IScpiInfoGetter
    {
        IEnumerable<CareCommandSubjectList> GetScpiSubjectCollections();
        List<Tuple<string, string, string>> GetMeterInfoList();
    }
}
