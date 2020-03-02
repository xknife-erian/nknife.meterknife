using System;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Tunnels.Handlers
{
    public class DUTProtocolHandler : CareProtocolHandler
    {
        private string _id;
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IMeasuringLogic _dataLogic;
        private readonly IAcquisitionService _acquisitionService;

        public DUTProtocolHandler(IMeasuringLogic dataLogic, IAcquisitionService acquisitionService)
        {
            _id = Guid.NewGuid().ToString("N").Substring(0, 4);
            _dataLogic = dataLogic;
            _acquisitionService = acquisitionService;
            Commands.Add(new byte[] { 0xAA, 0x00 });
            Commands.Add(new byte[] { 0xAB, 0x00 });
            //---
            Commands.Add(new byte[] { 0xAA, 0x01 });
        }

        public override void Received(CareTalking protocol)
        {
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                var dut = _dataLogic.GetDUT(protocol.DUT);
                if (double.TryParse(protocol.Scpi, out var value))
                {
                    _Logger.Trace($"Handler[{_id}]: {protocol.DUT} > {value}");
                    _acquisitionService.AddValue(dut, new MeasureData { Time = DateTime.Now, Data = value });
                }
            }
        }
    }
}
