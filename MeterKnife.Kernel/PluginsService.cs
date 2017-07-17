using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Interface;

namespace MeterKnife.Kernel
{
    public class PluginsService : IEnvironmentItem
    {
        public bool StartService()
        {
            throw new NotImplementedException();
        }

        public bool CloseService()
        {
            throw new NotImplementedException();
        }

        public int Order { get; }
        public string Description { get; }
    }
}
