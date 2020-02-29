using System;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Util.Serial.Common;
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
            _slot = new Slot();
            var config = new SerialConfig() {BaudRate = 115200};
            _slot.SetMeterCare(SlotType.Serial, (4, config));
            antService.Bind((_slot, connector));
            Pool = GetCommands();
        }

        public async Task RunAsync(IAntService antService, IEngineeringLogic engineeringLogic)
        {
            var dt = DateTime.Now;
            var engineering = new Engineering
            {
                Name = "",
                CreateTime = DateTime.Now//new DateTime(2019, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second),
            };
            engineering.CommandPools.Add(Pool);
            await engineeringLogic.CreateEngineeringAsync(engineering);
            await antService.StartAsync(engineering);
        }

        private ScpiCommandPool GetCommands()
        {
            var interval = 300;
            var item1 = new ScpiCommand
            {
                Slot = _slot,
                DUT = new DUT() {Id = $"RES{UtilRandom.GetString(2, UtilRandom.RandomCharType.Lowercased)}"},
                GpibAddress = 23,
                Scpi = new SCPI {Command = "FETC?"},

                Interval = interval,
                Timeout = interval * 2,
                IsLoop = true
            };
            item1.DUT.Name = item1.DUT.Id;
            var item2 = new ScpiCommand
            {
                Slot = _slot,
                DUT = new DUT() {Id = $"VOLTAGE{UtilRandom.GetString(2, UtilRandom.RandomCharType.Lowercased)}"},
                GpibAddress = 24,
                Scpi = new SCPI {Command = "READ?"},

                Interval = interval,
                Timeout = interval * 2,
                IsLoop = true
            };
            item2.DUT.Name = item2.DUT.Id;
            var temp5 = CareCommandHelper.Temperature(5);
            temp5.Slot = _slot;
            temp5.DUT = new DUT() {Id = $"T1{UtilRandom.GetString(2,UtilRandom.RandomCharType.Lowercased)}"};
            temp5.DUT.Name = temp5.DUT.Id;

            var temp6 = CareCommandHelper.Temperature(6);
            temp6.Slot = _slot;
            temp6.DUT = new DUT() {Id = $"T2{UtilRandom.GetString(2,UtilRandom.RandomCharType.Lowercased)}"};
            temp6.DUT.Name = temp6.DUT.Id;

            var pool = new ScpiCommandPool();
            //pool.Add(item1);
            //pool.AddRange(new[] { item1, item2, temp5 });
            pool.AddRange(new[] { item1, temp5 });
            //pool.AddRange(new[] {item2, temp6});
            return pool;
        }

    }
}