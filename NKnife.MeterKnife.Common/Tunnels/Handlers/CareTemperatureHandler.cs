﻿using System;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Tunnels.Handlers
{
    public class CareTemperatureHandler : CareProtocolHandler
    {
        private string _id;
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IMeasuringLogic _dataLogic;
        private readonly IAcquisitionService _acquisitionService;

        public CareTemperatureHandler(IMeasuringLogic dataLogic, IAcquisitionService acquisitionService)
        {
            _id = Guid.NewGuid().ToString("N").Substring(0, 4);
            _dataLogic = dataLogic;
            _acquisitionService = acquisitionService;
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
                _Logger.Trace($"TempHandler[{_id}]: {protocol.DUT} > {data.TrimEnd('\n')}");
                _acquisitionService.AddValue(dut, new MeasureData { Time = DateTime.Now, Data = value });
            }
        }
    }
}