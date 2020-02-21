using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.Care;
using NKnife.MeterKnife.Util.Serial.Common;
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
            /*
             * 启动采集的标准过程：
             * 1. 构建一个采集槽（Slot)；
             * 2. 在本软件的AntService中为该采集槽启用一个连接器（绑定）；
             * 3. 创建一个工程；
             * 4. 为这个工程创建运行外围（数据存储文件等）；
             * 5. 从AntService中启动这个工程；
             */
            _slot = new Slot();
            var config = new SerialConfig() { BaudRate = 115200 };
            _slot.SetMeterCare(SlotType.Serial, ((short)Port, config));

            _antService.Bind((_slot, _connector));
            var engineering = new Engineering();
            engineering.CommandPools.Add(GetCommands());
            await _engineeringLogic.CreateEngineeringAsync(engineering);
            await _antService.StartAsync(engineering);
        }

        private ScpiCommandPool GetCommands()
        {
            var interval = 1000;
            var item1 = new ScpiCommand
            {
                Slot = _slot,
                DUT = new DUT() {Id = "RES", Name = "520r|1K"},
                GpibAddress = 23,
                Scpi = new SCPI {Command = "FETC?"},

                Interval = interval,
                Timeout = interval*2,
                IsLoop = true
            };
            var item2 = new ScpiCommand
            {
                Slot = _slot,
                DUT = new DUT() {Id = "VOLTAGE", Name = "10v" },
                GpibAddress = 24,
                Scpi = new SCPI {Command = "READ?"},

                Interval = interval,
                Timeout = interval*2,
                IsLoop = true
            };
            var temp5 = CareCommandHelper.Temperature(5);
            temp5.Slot = _slot;
            temp5.DUT = new DUT() {Id = "T1", Name = "23Temp"};

            var temp6 = CareCommandHelper.Temperature(6);
            temp6.Slot = _slot;
            temp6.DUT = new DUT() {Id = "T2", Name = "24Temp"};

            var pool = new ScpiCommandPool();
            pool.AddRange(new[] {item2,temp5,item1,   temp6});
            return pool;
        }
    }
}