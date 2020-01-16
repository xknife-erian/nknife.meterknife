using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Winforms.Dialogs;
using MeterKnife.Instruments;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.App
{
    public partial class Workbench : Form
    {
        private readonly DockPanel _dockPanel = new DockPanel();

        private CommPort _carePort;
        private readonly IMeterKernel _meterKernel;
        private readonly DigitMultiMeterView _meterView;
        private readonly AboutDialog _aboutDialog;
        private AddMeterLiteDialog _addMeterLiteDialog;

        public Workbench(IMeterKernel meterKernel, DigitMultiMeterView meterView, AboutDialog aboutDialog, AddMeterLiteDialog addMeterLiteDialog)
        {
            this._meterKernel = meterKernel;
            this._meterView = meterView;
            _aboutDialog = aboutDialog;
            _addMeterLiteDialog = addMeterLiteDialog;
            InitializeComponent();
            InitializeDockPanel();

            meterKernel.Collected += (s, e) =>
            {
                _CareOptionMenuItem.Enabled = !e.IsCollected;
                _AddMeterMenuItem.Enabled = !e.IsCollected;
            };
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_dockPanel);

            _dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _dockPanel.Dock = DockStyle.Fill;
            _dockPanel.Theme = new VS2015BlueTheme();

            _dockPanel.BringToFront();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var dialog = new InterfaceSelectorDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _carePort = dialog.CarePort;
                _PortLabel.Text = dialog.IsSerial ? $"COM{dialog.CarePort}" : $"IPAddress:{dialog.CarePort}";
                AddMeterView();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            foreach (var dockContent in _dockPanel.Documents)
            {
                if (dockContent is MeterView collectView && !collectView.IsSaved)
                {
                    var sr = MessageBox.Show(this, $"{collectView.Text}有数据未保存，是否仍然关闭？",
                        $"{collectView.Text}未保存",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (sr == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                    collectView.IsSaved = true;
                }
            }
            base.OnClosing(e);
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
            Dictionary<CommPort, List<int>> dic = _meterKernel.GpibDictionary;
            if (!dic.TryGetValue(_carePort, out var gpibList))
            {
                gpibList = new List<int>();
                dic.Add(_carePort, gpibList);
            }

            _addMeterLiteDialog.GpibList.AddRange(gpibList);
            _addMeterLiteDialog.Port = _carePort;

            if (_addMeterLiteDialog.ShowDialog(this) == DialogResult.OK)
            {
                _meterView.SetMeter(_addMeterLiteDialog.Port, _addMeterLiteDialog.Meter);
                _meterView.Text = _addMeterLiteDialog.Meter.AbbrName;
                _meterView.Show(_dockPanel, DockState.Document);
                dic[_carePort].Add(_addMeterLiteDialog.GpibAddress);
            }
        }

        private void _CareOptionMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new CareParameterDialog(_carePort);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void _AboutMenuItem_Click(object sender, EventArgs e)
        {
            _aboutDialog.ShowDialog(this);
        }

        private void _LoggerMenuItem_Click(object sender, EventArgs e)
        {
//            var loggerForm = new NLogForm {TopMost = true};
//            loggerForm.Show();
        }
    }
}
