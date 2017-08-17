using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.MiscDemo
{
    public partial class KeysightChannelDemoView : DockContent
    {
        public KeysightChannelDemoView()
        {
            InitializeComponent();
            ControlStateCheck();
            ControlEventManager();
        }

        private void ControlEventManager()
        {
            _LoopEnableCheckBox.CheckedChanged += (s, e) => ControlStateCheck();
        }

        private void ControlStateCheck()
        {
            _LoopTimeBox.Enabled = _LoopEnableCheckBox.Checked;
        }
    }
}
