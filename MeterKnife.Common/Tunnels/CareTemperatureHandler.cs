using System;
using NKnife.MeterKnife.Common.Tunnels.CareOne;

namespace NKnife.MeterKnife.Common.Tunnels
{
    public class CareTemperatureHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPerformStorageLogic _TempStorage;

        public CareTemperatureHandler(IPerformStorageLogic tempStorage)
        {
            _TempStorage = tempStorage;
            Commands.Add(new byte[] {0xAE, 0x00});
        }

        public override void Received(CareTalking protocol)
        {
            var data = protocol.Scpi;
            _Logger.Debug($"Received TEMP:{data}");
            if (double.TryParse(data, out var yzl))
                _TempStorage.ProcessCurrentTemperature(new Temperature() {Time = DateTime.Now, Value = yzl});
        }
    }
}