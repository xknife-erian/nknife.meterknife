using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using NKnife.Tunnel;

namespace MeterKnife.Common.EventParameters
{
    public class InterfaceNodeClickedEventArgs : EventArgs
    {
        public BaseMeter Meter { get;  private set; }
        public CommPort Port { get; private set; }

        public InterfaceNodeClickedEventArgs(BaseMeter meter, CommPort port)
        {
            Meter = meter;
            Port = port;
        }
    }
}
