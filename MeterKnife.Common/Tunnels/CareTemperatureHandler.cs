using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Util.IoC;

namespace MeterKnife.Common.Tunnels
{
    public class CareTemperatureHandler : CareOneProtocolHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareConfigHandler>();

        public CareTemperatureHandler()
        {
            Commands.Add(new byte[] { 0xAE, 0x00 });
        }

        public override void Recevied(CareTalking protocol)
        {
            var tempService = DI.Get<ITemperatureService>();
            string data = protocol.Scpi;
            _logger.Debug(string.Format("Recevied TEMP:{0}", data));
            double yzl = 0;
            if (double.TryParse(data, out yzl))
            {
                tempService.TemperatureValues[0] = yzl;
            }
        }

    }
}
