using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Datas
{
    public class MeterDataService : IMeterDataService
    {
        public bool Save(string fileFullName, DataSet dataSet, IMeter meter)
        {
            return true;
        }
    }
}
