using System;
using NKnife.Events;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    // ReSharper disable once InconsistentNaming
    public class DUTProtocolHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPerformStorageLogic _storage;

        public DUTProtocolHandler(IPerformStorageLogic storage)
        {
            _storage = storage;
            Commands.Add(new byte[] { 0xAA, 0x00 });
            Commands.Add(new byte[] { 0xAB, 0x00 });
            //---
            Commands.Add(new byte[] { 0xAA, 0x01 });
        }

        public override async void Received(CareTalking protocol)
        {
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                var dut = _storage.GetDUT(protocol.Source);
                if (double.TryParse(protocol.Scpi, out var value))
                {
                    _Logger.Debug($"{protocol.GpibAddress} | {value}");
                    await _storage.ProcessAsync(dut, new MetricalData() { Time = DateTime.Now, Data = value });
                }
            }
        }
    }
}
