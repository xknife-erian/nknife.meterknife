using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Instruments;
using MeterKnife.Workbench.Dialogs;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Fairy
{
    public partial class FairyForm : Form
    {
        private readonly DockPanel _DockPanel = new DockPanel();

        private int _Serial;

        private IPAddress _IpAddress;

        private int _Port;

        public FairyForm()
        {
            InitializeComponent();
            InitializeDockPanel();
            AddGpibMeterDialog.IsFairy = true;
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_DockPanel);

            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var dialog = new InterfaceSelectorDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _Serial = dialog.Serial;
                _IpAddress = dialog.IpAddress;
                _Port = dialog.Port;
            }
        }

        private void _ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _AddMeterMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new AddGpibMeterDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var view = new DigitMultiMeterView();
                view.Show(_DockPanel, DockState.Document);
            }
        }

        private void _CareOptionMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new CareParameterDialog(1);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void _AboutMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new AboutDialog();
            dialog.ShowDialog(this);
        }
    }
}
