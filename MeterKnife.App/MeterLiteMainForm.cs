using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Winforms.Dialogs;
using MeterKnife.Instruments;
using NKnife.IoC;
using NKnife.NLog3.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.App.Lite
{
    public partial class MeterLiteMainForm : Form
    {
        private readonly DockPanel _dockPanel = new DockPanel();
        private readonly IMeterKernel _meterKernel = DI.Get<IMeterKernel>();

        private CommPort _carePort;

        public MeterLiteMainForm()
        {
            InitializeComponent();
            InitializeDockPanel();

            _meterKernel.Collected += (s, e) =>
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
                var collectView = dockContent as MeterView;
                if (collectView != null && !collectView.IsSaved)
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
            Dictionary<CommPort, List<int>> dic = DI.Get<IMeterKernel>().GpibDictionary;
            List<int> gpibList;
            if (!dic.TryGetValue(_carePort, out gpibList))
            {
                gpibList = new List<int>();
                dic.Add(_carePort, gpibList);
            }

            var dialog = new AddMeterLiteDialog();
            dialog.GpibList.AddRange(gpibList);
            dialog.Port = _carePort;

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var meterView = DI.Get<DigitMultiMeterView>();
                meterView.SetMeter(dialog.Port, dialog.Meter);
                meterView.Text = dialog.Meter.AbbrName;
                meterView.Show(_dockPanel, DockState.Document);
                dic[_carePort].Add(dialog.GpibAddress);
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
