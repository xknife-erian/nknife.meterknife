using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.Care;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("ci", Description = "连接Care，采集数据。")]
    public class CareCliCommand : BaseCommand
    {
        private readonly IAntService _antService;
        private readonly IDataConnector _connector;
        private readonly IEngineeringLogic _engineeringLogic;
        private Slot _slot;

        public CareCliCommand(IAntService antService, IDataConnector dataConnector, IEngineeringLogic engineeringLogic)
        {
            _antService = antService;
            _connector = dataConnector;
            _engineeringLogic = engineeringLogic;
        }

        public override async ValueTask ExecuteAsync(IConsole console)
        {
            _slot = Slot.Build(TunnelType.Serial, $"{Port}");
            _antService.Bind((_slot, _connector));
            var engineering = new Engineering
            {
                Commands = GetCommands()
            };
            await _engineeringLogic.CreateEngineering(engineering);
            await _antService.StartAsync(engineering);
        }

        private CareCommandPool GetCommands()
        {
            var interval = 1000;
            var item1 = new CareCommand
            {
                Slot = _slot,
                DUT = new DUT() {Id = "RES", Name = "520r|1K"},
                GpibAddress = 23,
                Scpi = new Scpi {Command = "FETC?"},

                Interval = interval,
                Timeout = interval*2,
                IsLoop = true
            };
            var item2 = new CareCommand
            {
                Slot = _slot,
                DUT = new DUT() {Id = "VOLTAGE", Name = "10v" },
                GpibAddress = 24,
                Scpi = new Scpi {Command = "READ?"},

                Interval = interval,
                Timeout = interval*2,
                IsLoop = true
            };
            var temp5 = CareScpiHelper.TEMP(5);
            temp5.Slot = _slot;
            temp5.DUT = new DUT() {Id = "T1", Name = "23Temp"};

            var temp6 = CareScpiHelper.TEMP(6);
            temp6.Slot = _slot;
            temp6.DUT = new DUT() {Id = "T2", Name = "24Temp"};

            var pool = new CareCommandPool();
            pool.AddRange(new[] {item1, item2, temp5, temp6});
            return pool;
        }
    }
}