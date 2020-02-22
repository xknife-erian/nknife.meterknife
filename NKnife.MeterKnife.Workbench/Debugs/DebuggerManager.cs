using System;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Holistic;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.MeterKnife.Workbench.Dialogs.Commands;
using NKnife.MeterKnife.Workbench.Dialogs.Engineerings;
using NKnife.MeterKnife.Workbench.Dialogs.Instruments;
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
        private readonly DataPlotView _measureView;

        private readonly IAntService _antService;
        private readonly IDataConnector _connector;
        private readonly IEngineeringLogic _engineeringLogic;

        public DebuggerManager(DataPlotView measureView, IAntService antService, IDataConnector connector, IEngineeringLogic engineeringLogic)
        {
            _measureView = measureView;
            _antService = antService;
            _connector = connector;
            _engineeringLogic = engineeringLogic;
        }

        public ToolStripMenuItem GetDebugMenu()
        {
            var debugMenu = new ToolStripMenuItem("研发调试(&D)");

            var menu = new ToolStripMenuItem("仪器编辑器");
            menu.Click += (sender, args) =>
            {
                var form = debugMenu.GetCurrentParent().FindForm();
                var dialog = Kernel.Container.Resolve<InstrumentDetailDialog>();
                dialog.ShowDialog(form);
            };
            debugMenu.DropDownItems.Add(menu);

            menu = new ToolStripMenuItem("工程编辑器");
            menu.Click += (sender, args) =>
            {
                var form = debugMenu.GetCurrentParent().FindForm();
                var dialog = Kernel.Container.Resolve<EngineeringDetailDialog>();
                dialog.ShowDialog(form);
            };
            debugMenu.DropDownItems.Add(menu);

            menu = new ToolStripMenuItem("指令编辑器");
            menu.Click += (sender, args) =>
            {
                var form = debugMenu.GetCurrentParent().FindForm();
                var dialog = Kernel.Container.Resolve<CommandEditorDialog>();
                dialog.ShowDialog(form);
            };
            debugMenu.DropDownItems.Add(menu);

            debugMenu.DropDownItems.Add(new ToolStripSeparator());

            menu = BuildMeasureMenu(debugMenu);
            menu.ShortcutKeys = Keys.F5;
            debugMenu.DropDownItems.Add(menu);

            return debugMenu;
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

                    var left = 0;
                    var right = 0;
                    var solution = new DUTSeriesStyleSolution();
                    for (var index = 0; index < start.Pool.Count; index++)
                    {
                        var cmd = start.Pool[index];
                        var style = DUTSeriesStyle.Build(LineStyle.Solid); //.GetAllLineStyles()[index];
                        style.Color = PlotTheme.CommonlyUsedColors[index];
                        style.DUT = cmd.DUT.Id;

                        style.Axis = new LinearAxis();
                        style.Axis.Key = cmd.DUT.Id;
                        style.Axis.FontSize = 13d;
                        style.Axis.MajorGridlineStyle = LineStyle.Dash;
                        style.Axis.MinorGridlineStyle = LineStyle.Dot;
                        style.Axis.MaximumPadding = 0;
                        style.Axis.MinimumPadding = 0;
                        style.Axis.Angle = 0;
                        style.Axis.Maximum = 220;
                        style.Axis.Minimum = -220;
                        if (index % 2 == 0)
                        {
                            style.Axis.AxisDistance = left++ * 60;
                            style.Axis.Position = AxisPosition.Left;
                        }
                        else
                        {
                            style.Axis.AxisDistance = right++ * 60;
                            style.Axis.Position = AxisPosition.Right;
                        }
                        solution.Add(style);
                    }

                    _measureView.SetStyleSolution(solution);
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
