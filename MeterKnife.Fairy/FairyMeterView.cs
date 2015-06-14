using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Instruments;
using ScpiKnife;

namespace MeterKnife.Fairy
{
    internal class FairyMeterView : DigitMultiMeterView
    {
        /// <summary>
        /// 是否是精灵版
        /// </summary>
        public static bool IsFairy { get; set; }

        public FairyMeterView()
        {
            if (IsFairy)
            {
                _ParamsGroupBox.Visible = false;
                _LeftSplitContainer.Panel1.Visible = false;
                _LeftSplitContainer.Panel1Collapsed = true;
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

        private string[] _InitCommands;
        private string _CollectCommand;

        protected override void StartCollect()
        {
            var dialog = new FairyCommandBuildDialog();
            if (string.IsNullOrEmpty(_CollectCommand))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _InitCommands = dialog.InitCommands.ToArray();
                    _CollectCommand = dialog.CollectCommand;
                }
                else
                {
                    return;
                }
            }
            base.StartCollect();
        }

        protected override ScpiCommandList GetInitCommands()
        {
            var list = new ScpiCommandList();
            foreach (var command in _InitCommands)
            {
                var sc = new ScpiCommand();
                sc.Command = command;
                list.AddLast(sc);
            }
            return list;
        }

        protected override byte[] GetCollectCommand()
        {
            return CareTalking.BuildCareSaying(_Meter.GpibAddress, _CollectCommand).Generate();
        }

    }
}
