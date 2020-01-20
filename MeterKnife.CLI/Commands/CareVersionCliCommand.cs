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
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("cv", Description = "连接Care，读取Care版本。")]
    public class CareVersionCliCommand : BaseCommand
    {
        private readonly ISlotService _slotService;
        private readonly ScpiProtocolHandler _handler;
        private readonly CareTemperatureHandler _tempHandler;
        private readonly SlotProcessor _slotProcessor;

        public CareVersionCliCommand(ISlotService slotService, ScpiProtocolHandler handler, CareTemperatureHandler tempHandler, SlotProcessor slotProcessor)
        {
            _slotService = slotService;
            _handler = handler;
            _tempHandler = tempHandler;
            _slotProcessor = slotProcessor;
        }

        public override async Task ExecuteAsync(IConsole console)
        {
            var slot = Slot.Build(TunnelType.Serial, $"{Port}");
            _slotService.Bind(slot, _slotProcessor, _handler, _tempHandler);
            _slotService.SendCommands(slot, GetCommands());
            _slotService.Start(slot);
        }

        private CareCommand[] GetCommands()
        {
            var item = new CareCommand
            {
                Content = new byte[] {0xf0, 0xf1, 0xf2},
                GpibAddress = 23,
                Heads = new Tuple<byte, byte>(0x10, 0x20),
                IsCare = true,
                Scpi = new Scpi {Command = "READ?"},

                Interval = 500,
                Timeout = 1000,
                IsLoop = true,
                Run = WriteLine
            };
            return new[] {item};
        }

        private bool WriteLine(IJob arg)
        {
            Console.WriteLine("+=+=+=...............");
            return true;
        }
    }
}
