using System.Threading.Tasks;
using CliFx.Attributes;
using CliFx.Services;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.Care;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("ci", Description = "连接Care，采集数据。")]
    public class CareCliCommand : BaseCommand
    {
        private readonly ISlotService _slotService;
        private readonly SlotProcessor _slotProcessor;
        private readonly IDbService _dbService;
        private readonly CareConfigHandler _configHandler;
        private readonly DUTProtocolHandler _handler;
        private readonly CareTemperatureHandler _tempHandler;

        public CareCliCommand(ISlotService slotService, SlotProcessor slotProcessor, DUTProtocolHandler handler, CareTemperatureHandler tempHandler, CareConfigHandler configHandler, IDbService dbService)
        {
            _slotService = slotService;
            _slotProcessor = slotProcessor;
            _handler = handler;
            _tempHandler = tempHandler;
            _configHandler = configHandler;
            _dbService = dbService;
            _dbService.SetConnections();
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
            var interval = 300;
            var item1 = new CareCommand
            {
                GpibAddress = 23,
                Scpi = new Scpi {Command = "FETC?"},

                Interval = interval,
                Timeout = 2000,
                IsLoop = true
            };
            var item2 = new CareCommand
            {
                GpibAddress = 24,
                Scpi = new Scpi {Command = "READ?"},

                Interval = interval,
                Timeout = 2000,
                IsLoop = true
            };
            // return new[] { item2 };
            // return new[] { item1 };
            // return new[] {CareScpiHelper.TEMP(1, 1000)};
            return new[] {item1, item2, CareScpiHelper.TEMP(5), CareScpiHelper.TEMP(6) };
        }
    }
}