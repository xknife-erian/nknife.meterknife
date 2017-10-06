using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Interfaces.Measures;
using MeterKnife.MiscDemo.Mocks;
using MeterKnife.Models;
using MeterKnife.Plots.Themes;
using MeterKnife.Plugins.ToolsMenu;
using MeterKnife.Plugins.ViewMenu.Loggers;
using MeterKnife.Views;
using MeterKnife.Views.Controls.Instruments;
using MeterKnife.Views.Measures.Series;
using NKnife.ControlKnife;
using NKnife.IoC;
using NKnife.Utility;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.MiscDemo
{
    public partial class MiscDemoMainForm : SimpleForm
    {
        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly UtilityRandom _Random = new UtilityRandom();

        public MiscDemoMainForm()
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
            //先向测量服务中添加模拟的被测物
            IMeasureService measureService = DI.Get<IMeasureService>();
            measureService.Exhibits.Clear();
            for (int i = 0; i < 99; i++)
            {
                var exhibit = new AbcExhibit();
                measureService.Exhibits.Add(exhibit);
            }

            var measureView = new MeasureView();
            measureView.Show(_DockPanel, DockState.Document);

            var startButton = new ToolStripMenuItem("Demo数据");
            startButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            startButton.Margin = new Padding(0, 3, 0, 2);
            startButton.Click += (s, x) =>
            {
                var builder = new MeasureDataRandomBuilder(measureView);
                builder.StartDemo();
            };
            measureView.AddDataToolStripItem(startButton);

            var pview = new PropertyGridView();
            pview.Show(_DockPanel, DockState.DockRightAutoHide);
            pview.SetObject1(measureView.GetMainPlotModel());
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

        private void _InstrumentCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new DockContent {Text = "Instrument Cell"};

            var head = new InstrumentsListHead();
            head.Location = new Point(20,20);
            head.GatewayModel = "Aglient82357B";
            head.Count = 99;
            view.Controls.Add(head);

            var clickCount = 0;
            var cell = new InstrumentCell(new Instrument("HP", "34401", "HP34401", 23));
            cell.Location = new Point(head.Location.X, head.Location.Y + head.Height + 5);
            view.Controls.Add(cell);
            view.Show(_DockPanel, DockState.Document);
            cell.CellMouseClicked += (s, x) =>
            {
                _TipStatusLabel.Text = $"{cell.Instrument.Address} -- {++clickCount} -- {Guid.NewGuid().ToString("N").Substring(0, 3)}";
            };

            var button = new Button();
            button.Text = "下一个";
            button.Size = new Size(60, 25);
            button.Location = new Point(cell.Location.X, cell.Location.Y + cell.Height + 12);
            view.Controls.Add(button);
            button.Click += (s, x) =>
            {
                var m = _Random.GetString(4, UtilityRandom.RandomCharType.Uppercased);
                var model = _Random.Next(1000, 99999);
                cell.Instrument = new Instrument(m, $"{model}", $"{m}{model}", _Random.Next(10, 99));
            };

        }

        private void _DataSeriesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //先向测量服务中添加模拟的被测物
            IMeasureService measureService = DI.Get<IMeasureService>();
            measureService.Exhibits.Clear();
            for (int i = 0; i < 30; i++)
            {
                var exhibit = new AbcExhibit();
                measureService.Exhibits.Add(exhibit);
            }

            var dialog = new DataSeriesListDialog();
            dialog.ShowDialog(this);
        }
    }
}