using System;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    public class CareTemperatureHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPerformStorageLogic _TempStorage;

        public CareTemperatureHandler(IPerformStorageLogic tempStorage)
        {
            _TempStorage = tempStorage;
            Commands.Add(new byte[] { 0xAE, 0x00 });
            Commands.Add(new byte[] { 0xAE, 0x01 });
            Commands.Add(new byte[] { 0xAE, 0x02 });
            Commands.Add(new byte[] { 0xAE, 0x03 });
        }

        public override void Received(CareTalking protocol)
        {
            var data = protocol.Scpi;
            _Logger.Debug($"Received TEMP:{data.TrimEnd('\n')}");
            if (double.TryParse(data, out var yzl))
                _TempStorage.ProcessCurrentTemperature(new Temperature() {Time = DateTime.Now, Value = yzl});
        }
    }
}