using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Dialogs;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class SlotView : SingletonDockContent
    {
        public SlotView()
        {
            InitializeComponent();
            RespondToClickEvent();
        }

        private void RespondToClickEvent()
        {
            _NewCareToolStripMenuItem.Click += (sender, args) =>
            {
                var dialog = new SerialPortSelectorDialog();
                dialog.Text = this.Res("选择MeterCare所在的串口");
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var result = dialog.SerialPort;
                }
            };
        }
    }
}
