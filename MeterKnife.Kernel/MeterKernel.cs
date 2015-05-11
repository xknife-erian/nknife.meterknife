using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Kernel
{
    public class MeterKernel : IMeterKernel
    {
        public Dictionary<int, List<int>> GpibDictionary { get; set; }
        public bool OnCollected { get; set; }

        public MeterKernel()
        {
            GpibDictionary = new Dictionary<int, List<int>>();
        }
    }
}
