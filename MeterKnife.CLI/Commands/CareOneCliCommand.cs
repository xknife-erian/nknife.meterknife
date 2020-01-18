using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Services;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.Util;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("care", Description = "连接MeterCare，尝试发送数据")]
    public class CareOneCliCommand : ICommand
    {
        private readonly BaseSlotService _service;
        private CareConfigHandler _configHandler;
        private CareTemperatureHandler _tempHandler;
        private ScpiProtocolHandler _protocolHandler;

        public CareOneCliCommand(BaseSlotService service, 
            CareConfigHandler configHandler, CareTemperatureHandler tempHandler, ScpiProtocolHandler protocolHandler)
        {
            _service = service;
            _configHandler = configHandler;
            _tempHandler = tempHandler;
            _protocolHandler = protocolHandler;
        }

        [CommandOption("port", 'p', IsRequired = true)]
        public ushort Port { get; set; }

        #region Implementation of ICommand

        public async Task ExecuteAsync(IConsole console)
        {
            var solt = Slot.Build(TunnelType.Serial, "3");
            _service.Bind(solt, _configHandler, _tempHandler, _protocolHandler);
            _service.Start(solt);
        }

        #endregion
    }
}