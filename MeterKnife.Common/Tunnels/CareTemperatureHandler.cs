using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels.CareOne;

namespace MeterKnife.Common.Tunnels
{
    public class CareTemperatureHandler : CareOneProtocolHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareConfigHandler>();
        private ITemperatureService _tempService;

        public CareTemperatureHandler(ITemperatureService tempService)
        {
            _tempService = tempService;
            Commands.Add(new byte[] { 0xAE, 0x00 });
        }

        public override void Recevied(CareTalking protocol)
        {
            string data = protocol.Scpi;
            _logger.Debug(string.Format("Recevied TEMP:{0}", data));
            double yzl = 0;
            if (double.TryParse(data, out yzl))
            {
                _tempService.TemperatureValues[0] = yzl;
            }
        }

    }
}
