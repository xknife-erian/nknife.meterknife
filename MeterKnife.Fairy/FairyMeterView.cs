using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Instruments;
using MeterKnife.Instruments.Common;
using ScpiKnife;

namespace MeterKnife.Fairy
{
    internal class FairyMeterView : DigitMultiMeterView
    {
        private readonly UserScpiCommandPanel _ScpiCommandPanel = new UserScpiCommandPanel();

        /// <summary>
        /// 是否是精灵版
        /// </summary>
        public static bool IsFairy { get; set; }

        public FairyMeterView()
        {
            if (IsFairy)
            {
                _ParamsGroupBox.Visible = false;
                _SaveStripButton.Visible = false;
                _ScpiCommandPanel.Dock = DockStyle.Fill;
                _LeftSplitContainer.Panel1.Controls.Clear();
                _LeftSplitContainer.Panel1.Controls.Add(_ScpiCommandPanel);
                _LeftSplitContainer.SplitterDistance = _ScpiCommandPanel.Height;
            }
        }

        public override void SetMeter(int port, BaseMeter meter)
        {
            base.SetMeter(port, meter);
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
                var sc = new ScpiCommand {Command = command};
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
