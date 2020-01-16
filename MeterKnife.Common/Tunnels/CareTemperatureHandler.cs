using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels.CareOne;

namespace MeterKnife.Common.Tunnels
{
    public class CareTemperatureHandler : CareOneProtocolHandler
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ITemperatureService _tempService;

        public CareTemperatureHandler(ITemperatureService tempService)
        {
            _tempService = tempService;
            Commands.Add(new byte[] {0xAE, 0x00});
        }

        public override void Received(CareTalking protocol)
        {
            var data = protocol.Scpi;
            _logger.Debug($"Received TEMP:{data}");
            if (double.TryParse(data, out var yzl))
                _tempService.TemperatureValues[0] = yzl;
        }
    }
}