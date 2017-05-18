using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huaxin.MultiTemperature.ViewModels.Entities;

namespace Huaxin.MultiTemperature.ViewModels
{
    public class Kernel
    {
        public Company WorkedCompany { get; set; }
        public MeterInfo WorkedMeter { get; set; }
        public string MeterageNumber { get; set; }
    }
}
