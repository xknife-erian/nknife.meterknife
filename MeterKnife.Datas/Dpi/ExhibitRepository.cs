using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Datas.Base;
using MeterKnife.Datas.Entities;

namespace MeterKnife.Datas.Dpi
{
    public class ExhibitRepository<T> : CrudRepositoryBase<ExhibitData<T>, uint>
    {
        public ExhibitRepository(string repositoryPath) 
            : base(repositoryPath)
        {
        }
    }
}
