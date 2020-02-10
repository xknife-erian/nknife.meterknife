using System;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Tunnels.Handlers
{
    public class DUTProtocolHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPerformStorageLogic _dataLogic;
        private readonly IMeasureService _measureService;

        public DUTProtocolHandler(IPerformStorageLogic dataLogic, IMeasureService measureService)
        {
            _dataLogic = dataLogic;
            _measureService = measureService;
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
                    _Logger.Trace($"{protocol.DUT} > {value}");
                    _measureService.AddValue(dut, new MeasureData { Time = DateTime.Now, Data = value });
                }
            }
        }
    }
}
