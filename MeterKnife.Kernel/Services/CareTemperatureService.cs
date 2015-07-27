using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using MeterKnife.Common;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Util;
using NKnife.IoC;

namespace MeterKnife.Kernel.Services
{
    public class CareTemperatureService 
    {
        private static readonly ILog _logger = LogManager.GetLogger<DataPathService>();
        public double[] TemperatureValues { get; set; }

        private readonly BaseCareCommunicationService _Comm = DI.Get<BaseCareCommunicationService>();
        private bool _IsStart = true;

        public bool StartService(CarePort carePort)
        {
            _IsStart = true;
            var task = new Task(() =>
            {
                while (_IsStart)
                {
                    _Comm.Send(carePort, 0, CommandUtil.TEMP());
                    Thread.Sleep(1000*5);
                }
            });
            task.Start();
            return true;
        }

        public bool CloseService()
        {
            _IsStart = false;
            return true;
        }
    }
}
