using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Services;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Common.Tunnels;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("cv", Description = "连接Care，读取Care版本。")]
    public class CareVersionCliCommand : BaseCommand
    {
        private readonly ISlotService _slotService;
        private readonly ScpiProtocolHandler _handler;

        public CareVersionCliCommand(ISlotService slotService, ScpiProtocolHandler handler)
        {
            _slotService = slotService;
            _handler = handler;
        }

        public override async Task ExecuteAsync(IConsole console)
        {
            var slot = Slot.Build(TunnelType.Serial, $"{Port}");
            _slotService.Bind(slot, _handler);
        }
    }
}
