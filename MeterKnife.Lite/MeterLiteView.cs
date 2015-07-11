using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.Controls;
using MeterKnife.Common.DataModels;
using MeterKnife.Instruments;
using MeterKnife.Instruments.Common;
using ScpiKnife;

namespace MeterKnife.Lite
{
    internal class MeterLiteView : DigitMultiMeterView
    {
        private readonly CustomerScpiCommandPanel _ScpiCommandPanel = new CustomerScpiCommandPanel();
        /// <summary>
        /// 是否是精灵版
        /// </summary>
        public static bool IsFairy { get; set; }

        public MeterLiteView()
        {
            if (IsFairy)
            {
                _PhotoToolStripButton.Visible = false;
                _ZoomInToolStripButton.Visible = false;
                _ZoomOutToolStripButton.Visible = false;
                _SaveStripButton.Visible = false;

                _ParamsGroupBox.Visible = false;
                _ScpiCommandPanel.Dock = DockStyle.Fill;
                _LeftSplitContainer.Panel2.Padding = new Padding(3, 2, 3, 2);
                _LeftSplitContainer.Panel2.Controls.Add(_ScpiCommandPanel);
            }
        }

        public override void SetMeter(CarePort port, BaseMeter meter)
        {
            base.SetMeter(port, meter);
            if (!IsFairy)
            {
                _Panel = meter.ParamPanel;
                _ParamsPanel.Controls.Add(_Panel);
            }
            if (!_Comm.IsInitialized)
            {
                _Comm.Start(port);
            }
        }

        protected override ScpiCommandList GetInitCommands()
        {
            var commands = _ScpiCommandPanel.GetInitCommands();
            return commands;
        }

        protected override ScpiCommandList GetCollectCommands()
        {
            var commands = _ScpiCommandPanel.GetCollectCommands();
            return commands;
        }

    }
}
