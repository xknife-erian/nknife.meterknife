using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.Util;
using MeterKnife.Instruments;
using MeterKnife.Workbench.Dialogs;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Fairy
{
    public partial class FairyForm : Form
    {
        private readonly DockPanel _DockPanel = new DockPanel();

        private int _Serial;

        private IPAddress _IpAddress;

        private int _Port;

        private CommunicationType _CommunicationType;

        public FairyForm()
        {
            InitializeComponent();
            InitializeDockPanel();
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
                if (dialog.IsSerial)
                {
                    _PortLabel.Text = string.Format("COM{0}", dialog.Serial);
                    _CommunicationType = CommunicationType.Serial;
                }
                else
                {
                    _PortLabel.Text = string.Format("{0}:{1}", dialog.IpAddress, dialog.Port);
                    _CommunicationType = CommunicationType.Socket;
                }
                AddMeterView();
            }
        }

        private void _ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _AddMeterMenuItem_Click(object sender, EventArgs e)
        {
            AddMeterView();
        }

        private void AddMeterView()
        {
            var dialog = new AddFairyMeterDialog();
            dialog.Port = _Serial;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var meterView = DI.Get<FairyMeterView>();
                meterView.Port = dialog.Port;
                meterView.CommunicationType = _CommunicationType;
                meterView.SetMeter(dialog.Port, dialog.Meter);
                meterView.Text = dialog.Meter.AbbrName;
                meterView.Show(_DockPanel, DockState.Document);
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
