using System;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.Util;

namespace NKnife.MeterKnife.Workbench.Debugs
{

    public class SimpleMeasure
    {
        private Slot _slot;
        public ScpiCommandPool Pool { get; private set; }

        /*
         * 启动采集的标准过程：
         * 1. 构建一个采集槽（Slot)；
         * 2. 在本软件的AntService中为该采集槽启用一个连接器（绑定）；
         * 3. 创建一个工程；
         * 4. 为这个工程创建运行外围（数据存储文件等）；
         * 5. 从AntService中启动这个工程；
         */

        public void Init(IAntService antService, IDataConnector connector)
        {
            _slot = Slot.Build(TunnelType.Serial, $"4");
            antService.Bind((_slot, connector));
            Pool = GetCommands();
        }

        public async Task RunAsync(IAntService antService, IEngineeringLogic engineeringLogic)
        {
            var engineering = new Engineering
            {
                Name = "",
                CreateTime = new DateTime(2019, UtilRandom.Next(11, 13), UtilRandom.Next(1, 25), UtilRandom.Next(1, 24), UtilRandom.Next(1, 60), UtilRandom.Next(1, 60)),
                Commands = Pool
            };
            await engineeringLogic.CreateEngineeringAsync(engineering);
            await antService.StartAsync(engineering);
        }

        private ScpiCommandPool GetCommands()
        {
            var interval = 1000;
            var item1 = new ScpiCommand
            {
                Slot = _slot,
                DUT = new DUT() { Id = "RES", Name = "520r|1K" },
                GpibAddress = 23,
                Scpi = new Scpi { Command = "FETC?" },

                Interval = interval,
                Timeout = interval * 2,
                IsLoop = true
            };
            var item2 = new ScpiCommand
            {
                Slot = _slot,
                DUT = new DUT() { Id = "VOLTAGE", Name = "10v" },
                GpibAddress = 24,
                Scpi = new Scpi { Command = "READ?" },

                Interval = interval,
                Timeout = interval * 2,
                IsLoop = true
            };
            var temp5 = ScpiHelper.Temperature(5);
            temp5.Slot = _slot;
            temp5.DUT = new DUT() { Id = "T1", Name = "23Temp" };

            var temp6 = ScpiHelper.Temperature(6);
            temp6.Slot = _slot;
            temp6.DUT = new DUT() { Id = "T2", Name = "24Temp" };

            var pool = new ScpiCommandPool();
            pool.AddRange(new[] { item2, temp5 });
            pool.AddRange(new[] { item1, temp6 });
            return pool;
        }

    }
}