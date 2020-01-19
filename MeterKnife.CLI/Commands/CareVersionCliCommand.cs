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
using NKnife.MeterKnife.Util.Scpi;
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("cv", Description = "连接Care，读取Care版本。")]
    public class CareVersionCliCommand : BaseCommand
    {
        private readonly ISlotService _slotService;
        private readonly IDataConnector _connector;
        private readonly ScpiProtocolHandler _handler;
        private readonly CareTemperatureHandler _tempHandler;

        public CareVersionCliCommand(ISlotService slotService, IDataConnector connector, 
            ScpiProtocolHandler handler, CareTemperatureHandler tempHandler)
        {
            _slotService = slotService;
            _handler = handler;
            _connector = connector;
            _tempHandler = tempHandler;
        }

        public override async Task ExecuteAsync(IConsole console)
        {
            var cmdPair = GetLoopCommands();
            var slot = Slot.Build(TunnelType.Serial, $"{Port}");
            _slotService.Bind(slot, _connector, _handler, _tempHandler);
            _slotService.SendLoopCommands(slot, cmdPair.Key, cmdPair.Value);
            _slotService.Start(slot);
        }

        private KeyValuePair<string, ScpiCommandQueue.Item[]> GetLoopCommands()
        {
            return new KeyValuePair<string, ScpiCommandQueue.Item[]>("abc", GetCommands());
        }

        private ScpiCommandQueue.Item[] GetCommands()
        {
            var item = new ScpiCommandQueue.Item
            {
                Content = new byte[] {0xf0, 0xf1, 0xf2},
                GpibAddress = 23,
                Heads = new Tuple<byte, byte>(0x10, 0x20),
                Interval = 500,
                IsCare = true,
                ScpiCommand = new ScpiCommand {Command = "READ?"}
            };
            return new[] {item};
        }
    }
}
