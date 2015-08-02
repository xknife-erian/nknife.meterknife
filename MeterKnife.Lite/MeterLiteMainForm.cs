using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using MeterKnife.Workbench.Dialogs;
using NKnife.IoC;
using NKnife.NLog3.Controls;
using NKnife.Tunnel;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Lite
{
    public partial class MeterLiteMainForm : Form
    {
        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly IMeterKernel _MeterKernel = DI.Get<IMeterKernel>();

        private CommPort _CarePort;

        public MeterLiteMainForm()
        {
            InitializeComponent();
            InitializeDockPanel();

            _MeterKernel.Collected += (s, e) =>
            {
                _CareOptionMenuItem.Enabled = !e.IsCollected;
                _AddMeterMenuItem.Enabled = !e.IsCollected;
            };
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
                _CarePort = dialog.CarePort;
                _PortLabel.Text = dialog.IsSerial ? 
                    string.Format("COM{0}", dialog.CarePort) :
                    string.Format("IPAddress:{0}", dialog.CarePort);
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
            Dictionary<CommPort, List<int>> dic = DI.Get<IMeterKernel>().GpibDictionary;
            List<int> gpibList;
            if (!dic.TryGetValue(_CarePort, out gpibList))
            {
                gpibList = new List<int>();
                dic.Add(_CarePort, gpibList);
            }

            var dialog = new AddMeterLiteDialog();
            dialog.GpibList.AddRange(gpibList);
            dialog.Port = _CarePort;

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var meterView = DI.Get<MeterLiteView>();
                meterView.SetMeter(dialog.Port, dialog.Meter);
                meterView.Text = dialog.Meter.AbbrName;
                meterView.Show(_DockPanel, DockState.Document);
                dic[_CarePort].Add(dialog.GpibAddress);
            }
        }

        private void _CareOptionMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new CareParameterDialog(_CarePort);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void _AboutMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new AboutDialog();
            dialog.ShowDialog(this);
        }

        private void _LoggerMenuItem_Click(object sender, EventArgs e)
        {
            var loggerForm = new NLogForm {TopMost = true};
            loggerForm.Show();
        }
    }
}
