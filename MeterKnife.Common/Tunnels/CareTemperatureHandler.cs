using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels.CareOne;

namespace MeterKnife.Common.Tunnels
{
    public class CareTemperatureHandler : CareOneProtocolHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareConfigHandler>();

        public override void Recevied(CareTalking protocol)
        {
            throw new NotImplementedException();
        }
    }
}
