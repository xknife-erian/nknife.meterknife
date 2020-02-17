using System;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Holistic;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.MeterKnife.Workbench.Dialogs.Engineerings;
using NKnife.MeterKnife.Workbench.Views;
using NKnife.Util;
using NKnife.Win.Quick.Base;
using OxyPlot;
using OxyPlot.Axes;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Debugs
{
    public class DebuggerManager
    {
        private readonly MeasureView _measureView;

        private readonly IAntService _antService;
        private readonly IDataConnector _connector;
        private readonly IEngineeringLogic _engineeringLogic;

        public DebuggerManager(MeasureView measureView, IAntService antService, IDataConnector connector, IEngineeringLogic engineeringLogic)
        {
            _measureView = measureView;
            _antService = antService;
            _connector = connector;
            _engineeringLogic = engineeringLogic;
        }

        public ToolStripMenuItem GetDebugMenu()
        {
            var debugMainMenu = new ToolStripMenuItem("Debug");
            var plot = BuildMeasureMenu(debugMainMenu);
            debugMainMenu.DropDownItems.Add(plot);
            var cmdEditDilog = BuildCommandEditMenu(debugMainMenu);
            debugMainMenu.DropDownItems.Add(cmdEditDilog);
            return debugMainMenu;
        }

        private ToolStripMenuItem BuildCommandEditMenu(ToolStripMenuItem debugMainMenu)
        {
            var menu = new ToolStripMenuItem("指令编辑器");
            menu.Click += (sender, args) =>
            {
                var form = menu.GetCurrentParent().FindForm(); 
                var dialog = Kernel.Container.Resolve<CareCommandEditorDialog>();
                dialog.ShowDialog(form);
            };
            return menu;
        }

        private ToolStripMenuItem BuildMeasureMenu(ToolStripMenuItem debug)
        {
            var plot = new ToolStripMenuItem("测量曲线窗体");
            plot.Click += async (s, e) =>
            {
                var form = debug.GetCurrentParent().FindForm();
                if (form != null && form is IWorkbench wb)
                {
                    var start = new SimpleMeasure();
                    start.Init(_antService, _connector);

                    var solution = new DUTSeriesStyleSolution();
                    for (var index = 0; index < start.Pool.Count; index++)
                    {
                        var cmd = start.Pool[index];
                        var style = DUTSeriesStyle.GetAllLineStyles()[index];
                        style.Color = DUTSeriesStyle.AllLineColors[index];
                        style.DUT = cmd.DUT.Id;

                        style.Axis = new LinearAxis();
                        style.Axis.Key = cmd.DUT.Id;
                        style.Axis.FontSize = 13d;
                        style.Axis.AxisDistance = index * 60;
                        style.Axis.MajorGridlineStyle = LineStyle.Dash;
                        style.Axis.MinorGridlineStyle = LineStyle.Dot;
                        style.Axis.MaximumPadding = 0;
                        style.Axis.MinimumPadding = 0;
                        style.Axis.Angle = 0;
                        style.Axis.Maximum = 220;
                        style.Axis.Minimum = -220;
                        style.Axis.Position = AxisPosition.Left;
                        solution.Add(style);
                    }

                    _measureView.ViewModel.StyleSolution = solution;
                    _measureView.Show(wb.MainDockPanel, DockState.Document);
                    await start.RunAsync(_antService, _engineeringLogic);
                }
            };
            return plot;
        }

        public static ToolStripMenuItem GetMockItem()
        {
            var word = Guid.NewGuid().ToString("N").ToUpper();
            var t = new ToolStripMenuItem($"{word.Substring(0, UtilRandom.Next(4, 8))}(&{word[0]})");
            t.Click += (s, e) => { MessageBox.Show(word); };
            return t;
        }
    }
}
