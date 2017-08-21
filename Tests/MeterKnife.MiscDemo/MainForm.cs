using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Plots.Themes;
using MeterKnife.Plugins.FileMenu;
using MeterKnife.Plugins.ViewMenu.Loggers;
using MeterKnife.Views.InstrumentsDiscovery;
using MeterKnife.Views.Measures;
using NKnife.ControlKnife;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.MiscDemo
{
    public partial class MainForm : SimpleForm
    {
        private readonly DockPanel _DockPanel = new DockPanel();

        public MainForm()
        {
            InitializeComponent();
            InitializeDockPanel();
        }

        private void InitializeDockPanel()
        {
            _DockPanel.Theme = new VS2015BlueTheme();
            _DockPanel.Dock = DockStyle.Fill;
            Controls.Add(_DockPanel);
            _DockPanel.BringToFront();

            var view = DI.Get<LoggerView>();
            view.Show(_DockPanel, DockState.DockBottomAutoHide);
        }

        private void _MainPlotTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeasureView view = new MeasureView();
            view.Show(_DockPanel, DockState.Document);

            var pview = new PropertyGridView();
            pview.Show(_DockPanel, DockState.DockRightAutoHide);
            pview.SetObject1(view.GetMainPlotModel());
            pview.SetObject2(view.GetMainPlotModel().Series[0]);
        }

        private void _ThemeManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new ThemeManagerDialog();
            dialog.ShowDialog(this);
        }

        private void _KeysightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = DI.Get<LoggerView>();
            view.Show(_DockPanel, DockState.DockBottom);
            var ksView = new KeysightChannelDemoView();
            ksView.Show(_DockPanel, DockState.Document);
        }

        private void _InstrumentsDiscoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ksView = new InstrumentsDiscoveryView();
            ksView.Show(_DockPanel, DockState.Document);
        }

        private void _InstrumentsPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new DockContent();
            view.Text = "仪器管理控件";
            var panel = new InstrumentsPanel();
            panel.BackColor = Color.Coral;
            panel.Dock = DockStyle.Fill;
            view.Controls.Add(panel);
            view.Show(_DockPanel, DockState.Document);
        }
    }
}
