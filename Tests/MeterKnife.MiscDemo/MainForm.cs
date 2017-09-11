using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Models;
using MeterKnife.Plots.Themes;
using MeterKnife.Plugins.ToolsMenu;
using MeterKnife.Plugins.ViewMenu.Loggers;
using MeterKnife.Views.InstrumentsDiscovery;
using MeterKnife.Views.InstrumentsDiscovery.Controls;
using MeterKnife.Views.InstrumentsDiscovery.Controls.Datas;
using MeterKnife.Views.InstrumentsDiscovery.Controls.Instruments;
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
            var ksView = new KeysightChannelToolView();
            ksView.Show(_DockPanel, DockState.Document);
        }

        private void _InstrumentsDiscoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ksView = new InstrumentsDiscoveryView();
            ksView.Show(_DockPanel, DockState.Document);
        }

        private void _InstrumentDatasListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new DockContent {Text = "仪器数据管理控件"};
            var dataslistPanel = new DatasListPanel {Dock = DockStyle.Fill};
            view.Controls.Add(dataslistPanel);
            view.Show(_DockPanel, DockState.Document);
        }

        private void _InstrumentCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var clickCount = 0;
            var view = new DockContent {Text = "Instrument Cell" };
            var cell = new InstrumentCell(new Instrument("HP", "34401", "HP34401", 23));
            cell.Location = new Point(20, 20);
            view.BackColor = Color.MidnightBlue;
            view.Controls.Add(cell);
            view.Show(_DockPanel, DockState.Document);
            cell.BackColor = Color.Beige;
            cell.CellMouseClicked += (s, x) =>
            {
                _TipStatusLabel.Text = $"{cell.Address} -- {++clickCount} -- {Guid.NewGuid().ToString("N").Substring(0, 3)}";
            };
        }
    }
}