using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;

namespace MeterKnife.Cares
{
    public class CareOneDiscover : GatewayDiscoverBase
    {
        #region Implementation of IGatewayDiscover

        /// <summary>
        /// 本发现器的测量途径模式
        /// </summary>
        public override GatewayModel GatewayModel { get; set; } = GatewayModel.CareOne;

        private int _DemoCount = 1;

        /// <summary>
        /// 手动添加仪器
        /// </summary>
        public override void CreateInstrument()
        {
            var inst = new Instrument("HP", "34401", "HP34401", 22);
            inst.DatasCount = _DemoCount++;
            Instruments.Add(inst);
        }

        /// <summary>
        /// 开始搜索该测量途径下的所有仪器
        /// </summary>
        public override void BeginDiscover()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}