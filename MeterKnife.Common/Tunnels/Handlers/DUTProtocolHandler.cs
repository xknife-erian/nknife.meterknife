using System;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Tunnels.Handlers
{
    // ReSharper disable once InconsistentNaming
    public class DUTProtocolHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPerformStorageLogic _dataLogic;

        public DUTProtocolHandler(IPerformStorageLogic dataLogic)
        {
            _dataLogic = dataLogic;
            Commands.Add(new byte[] { 0xAA, 0x00 });
            Commands.Add(new byte[] { 0xAB, 0x00 });
            //---
            Commands.Add(new byte[] { 0xAA, 0x01 });
        }

        public override async void Received(CareTalking protocol)
        {
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                var dut = _dataLogic.GetDUT(protocol.DUT);
                if (double.TryParse(protocol.Scpi, out var value))
                {
                    _Logger.Debug($"{protocol.DUT} | {value}");
                    await _dataLogic.ProcessAsync(dut, new MetricalData() { Time = DateTime.Now, Data = value });
                }
            }
        }
    }
}
