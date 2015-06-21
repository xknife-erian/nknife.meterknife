using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Instruments;
using MeterKnife.Instruments.Common;
using ScpiKnife;

namespace MeterKnife.Lite
{
    internal class MeterLiteView : DigitMultiMeterView
    {
        private readonly UserScpiCommandPanel _ScpiCommandPanel = new UserScpiCommandPanel();
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
                _ParamsGroupBox.Visible = false;
                _SaveStripButton.Visible = false;
                _ScpiCommandPanel.Dock = DockStyle.Fill;
                _LeftSplitContainer.Panel1.Controls.Add(_ScpiCommandPanel);
            }
        }

        public override void SetMeter(int port, BaseMeter meter)
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
            var commands = _ScpiCommandPanel.InitCommands;
            var list = new ScpiCommandList();
            foreach (var command in commands)
            {
                var sc = new ScpiCommand();
                sc.Command = command;
                list.AddLast(sc);
            }
            return list;
        }

        protected override List<byte[]> GetCollectCommands()
        {
            var commands = _ScpiCommandPanel.CollectCommands;
            return commands.Select(command => CareTalking.BuildCareTalking(_Meter.GpibAddress, command).Generate()).ToList();
        }

    }
}
