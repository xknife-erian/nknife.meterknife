using System;
using System.Collections.Generic;
using MeterKnife.Base;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.Utility;

namespace MeterKnife.Cares
{
    public class CareOneDiscover : GatewayDiscoverBase
    {
        #region Implementation of IGatewayDiscover

        /// <summary>
        ///     本发现器的测量途径模式
        /// </summary>
        public override GatewayModel GatewayModel { get; set; } = GatewayModel.CareOne;

        private int _demoCount = 1;
        private readonly UtilityRandom _random = new UtilityRandom();

        /// <summary>
        ///     手动添加仪器
        /// </summary>
        public override void CreateInstrument()
        {
            var model = $"344{_random.Next(10, 99)}";
            var inst = new Instrument("HP", model, $"HP{model}", _random.Next(1, 36));
            inst.DatasCount = _demoCount++;
            Instruments.Add(inst);
        }

        /// <summary>
        ///     开始搜索该测量途径下的所有仪器
        /// </summary>
        public override void BeginDiscover()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     刷新本测量途径挂接的仪器或设备列表
        /// </summary>
        public override List<InstrumentConnectionState> Refresh()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}