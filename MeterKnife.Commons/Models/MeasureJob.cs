using System.Collections.Generic;
using MeterKnife.Base;

namespace MeterKnife.Models
{
    public class MeasureJob
    {
        public string Number { get; set; }
        public List<ExhibitBase> Exhibits { get; set; }
    }
}