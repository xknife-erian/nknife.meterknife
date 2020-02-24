using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Metrology;

namespace NKnife.MeterKnife.Util.Metrology
{
    public class MetrologyHelper
    {
        public static IReadOnlyCollection<IMetrology> Metrologies { get; }

        static MetrologyHelper()
        {
            var list = new List<IMetrology>();
            list.AddRange(new IMetrology[]
            {
                new ElectricResistance(), 
                new ElectricVoltage(),
                new ElectricCurrent(), 
                new ElectricCapacitance(), 
                new ElectricInductance(), 
                new Frequency(), 
                new Temperature(), 
            });
            Metrologies = Array.AsReadOnly(list.ToArray());
        }
    }
}
