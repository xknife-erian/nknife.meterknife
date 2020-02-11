using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.Workbench.Debugs
{
    public class SimpleMeasure
    {
        private Slot _slot;

        public async Task RunAsync(IAntService antService, IDataConnector connector, IEngineeringLogic engineeringLogic)
        {
            /*
             * 启动采集的标准过程：
             * 1. 构建一个采集槽（Slot)；
             * 2. 在本软件的AntService中为该采集槽启用一个连接器（绑定）；
             * 3. 创建一个工程；
             * 4. 为这个工程创建运行外围（数据存储文件等）；
             * 5. 从AntService中启动这个工程；
             */
            _slot = Slot.Build(TunnelType.Serial, $"4");
            antService.Bind((_slot, connector));
            var engineering = new Engineering
            {
                Commands = GetCommands()
            };
            await engineeringLogic.CreateEngineering(engineering);
            await antService.StartAsync(engineering);
        }

        private CareCommandPool GetCommands()
        {
            var interval = 1000;
            var item1 = new CareCommand
            {
                Slot = _slot,
                DUT = new DUT() { Id = "RES", Name = "520r|1K" },
                GpibAddress = 23,
                Scpi = new Scpi { Command = "FETC?" },

                Interval = interval,
                Timeout = interval * 2,
                IsLoop = true
            };
            var item2 = new CareCommand
            {
                Slot = _slot,
                DUT = new DUT() { Id = "VOLTAGE", Name = "10v" },
                GpibAddress = 24,
                Scpi = new Scpi { Command = "READ?" },

                Interval = interval,
                Timeout = interval * 2,
                IsLoop = true
            };
            var temp5 = CareScpiHelper.TEMP(5);
            temp5.Slot = _slot;
            temp5.DUT = new DUT() { Id = "T1", Name = "23Temp" };

            var temp6 = CareScpiHelper.TEMP(6);
            temp6.Slot = _slot;
            temp6.DUT = new DUT() { Id = "T2", Name = "24Temp" };

            var pool = new CareCommandPool();
            pool.AddRange(new[] { item2, temp5, item1, temp6 });
            return pool;
        }
    }
}