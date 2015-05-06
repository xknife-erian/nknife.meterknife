﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Util;

namespace MeterKnife.Common.EventParameters
{
    public class InterfaceNodeClickedEventArgs : EventArgs
    {
        public IMeter Meter { get;  private set; }
        public int Port { get; private set; }

        public CommunicationType CommunicationType { get; private set; }

        public InterfaceNodeClickedEventArgs(IMeter meter, int port, CommunicationType commType)
        {
            Meter = meter;
            Port = port;
            CommunicationType = commType;
        }
    }
}