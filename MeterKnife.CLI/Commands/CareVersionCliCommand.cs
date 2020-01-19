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
            console.ForegroundColor = ConsoleColor.Yellow;
            console.Output.WriteLine($"Care已连接...");
            console.Output.WriteLine($"循环发送...");
            console.ResetColor();
            var cmdPair = GetLoopCommands();
            _slotService.SendLoopCommands(slot, cmdPair.Key, cmdPair.Value);
            _slotService.Bind(slot, _handler);
            _slotService.Start(slot);
        }

        private KeyValuePair<string, ScpiCommandQueue.Item[]> GetLoopCommands()
        {
            return new KeyValuePair<string, ScpiCommandQueue.Item[]>("abc", GetCommands());
        }

        private ScpiCommandQueue.Item[] GetCommands()
        {
            var item = new ScpiCommandQueue.Item();
            item.Content = new byte[] {0xf0, 0xf1, 0xf2};
            item.GpibAddress = 23;
            item.Heads = new Tuple<byte, byte>(0x10, 0x20);
            item.Interval = 50;
            item.IsCare = true;
            item.ScpiCommand = new ScpiCommand();
            item.ScpiCommand.Command = "READ?";
            return new ScpiCommandQueue.Item[] {item};
        }
    }
}
