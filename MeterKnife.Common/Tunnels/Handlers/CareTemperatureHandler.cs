using System;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Tunnels.Handlers
{
    public class CareTemperatureHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPerformStorageLogic _dataLogic;

        public CareTemperatureHandler(IPerformStorageLogic dataLogic)
        {
            _dataLogic = dataLogic;
            Commands.Add(new byte[] {0xAE, 0x00});
            Commands.Add(new byte[] {0xAE, 0x01});
            Commands.Add(new byte[] {0xAE, 0x02});
            Commands.Add(new byte[] {0xAE, 0x03});
            Commands.Add(new byte[] {0xAE, 0x04});
            Commands.Add(new byte[] {0xAE, 0x05});
            Commands.Add(new byte[] {0xAE, 0x06});
        }

        public override async void Received(CareTalking protocol)
        {
            var data = protocol.Scpi;
            var dut = _dataLogic.GetDUT(protocol.DUT);
            if (double.TryParse(data, out var value))
            {
                _Logger.Debug($"Received TEMP: {protocol.DUT} {data.TrimEnd('\n')}");
                await _dataLogic.ProcessAsync(dut, new MetricalData() {Time = DateTime.Now, Data = value});
            }
        }
    }
}