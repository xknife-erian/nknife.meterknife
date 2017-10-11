using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;
using MeterKnife.Models.Datas;

namespace MeterKnife.Datas.Dpi
{
    public class ExhibitRepository<T> : GlobalDataBaseRepositoryBase<ExhibitData<T>, uint>
    {
    }
}
