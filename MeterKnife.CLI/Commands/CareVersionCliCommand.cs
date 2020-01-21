using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Services;
using NKnife.Interface;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.Care;
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("cv", Description = "连接Care，读取Care版本。")]
    public class CareVersionCliCommand : BaseCommand
    {
        private readonly ISlotService _slotService;
        private readonly SlotProcessor _slotProcessor;
        private readonly ScpiProtocolHandler _handler;
        private readonly CareTemperatureHandler _tempHandler;
        private readonly CareConfigHandler _configHandler;

        public CareVersionCliCommand(ISlotService slotService, SlotProcessor slotProcessor, ScpiProtocolHandler handler, CareTemperatureHandler tempHandler, CareConfigHandler configHandler)
        {
            _slotService = slotService;
            _slotProcessor = slotProcessor;
            _handler = handler;
            _tempHandler = tempHandler;
            _configHandler = configHandler;
        }

        public override async Task ExecuteAsync(IConsole console)
        {
            var slot = Slot.Build(TunnelType.Serial, $"{Port}");
            _slotService.Bind(slot, _slotProcessor, _handler, _tempHandler, _configHandler);
            _slotService.SendCommands(slot, GetCommands());
            _slotService.Start(slot);
        }

        private CareCommand[] GetCommands()
        {
            var item = new CareCommand
            {
                GpibAddress = 23,
                Scpi = new Scpi {Command = "FETC?"},

                Interval = 600,
                Timeout = 2000,
                IsLoop = true,
            };
            return new[] {item};
        }
    }
}
