﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Scpi;

namespace MeterKnife.Scpis
{
    public interface IScpiInfoGetter
    {
        IEnumerable<ScpiSubjectCollection> GetScpiSubjectCollections();
        List<Tuple<string, string, string>> GetMeterInfoList();
    }
}
