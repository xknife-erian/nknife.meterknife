using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks.Dataflow;
using NKnife.MeterKnife.Slots;

namespace NKnife.MeterKnife.Tests.Infrastructure
{
    public class AppMockRunner : IRunner
    {
        #region Implementation of IRunner

        public void Run(IPackager packager, CommandMode commandMode, string body)
        {
        }

        #endregion
    }
}
