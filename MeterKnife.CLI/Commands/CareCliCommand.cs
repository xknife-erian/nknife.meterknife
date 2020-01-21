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
    [Command("ci", Description = "连接Care，采集数据。")]
    public class CareCliCommand : BaseCommand
    {
        private readonly ISlotService _slotService;
        private readonly SlotProcessor _slotProcessor;
        private readonly DUTProtocolHandler _handler;
        private readonly CareTemperatureHandler _tempHandler;
        private readonly CareConfigHandler _configHandler;

        public CareCliCommand(ISlotService slotService, SlotProcessor slotProcessor, DUTProtocolHandler handler, CareTemperatureHandler tempHandler, CareConfigHandler configHandler)
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
            var interval = 500;
            var item1 = new CareCommand
            {
                GpibAddress = 23,
                Scpi = new Scpi {Command = "FETC?"},

                Interval = interval,
                Timeout = 2000,
                IsLoop = true,
            };
            var item2 = new CareCommand
            {
                GpibAddress = 24,
                Scpi = new Scpi {Command = "READ?"},

                Interval = interval,
                Timeout = 2000,
                IsLoop = true,
            };
            // return new[] { item2 };
            // return new[] { item1 };
            //return new[] {CareScpiHelper.TEMP(1, 1000)};
            return new[] {item1, item2, CareScpiHelper.TEMP(0), CareScpiHelper.TEMP(1)};
        }
    }
}
