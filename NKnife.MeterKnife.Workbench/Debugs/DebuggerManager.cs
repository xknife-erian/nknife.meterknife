using System;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Scpi;
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
        private readonly RealTimeDataPlotView _realTimeDataPlotView;

        private readonly IWorkbenchViewModel _workbenchViewModel;

        public DebuggerManager(RealTimeDataPlotView realTimeDataPlotView, IWorkbenchViewModel workbenchViewModel)
        {
            _realTimeDataPlotView = realTimeDataPlotView;
            _workbenchViewModel = workbenchViewModel;
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
            menu.ShortcutKeys = Keys.F5;
            menu.Click += (sender, args) =>
            {
                var form = debugMenu.GetCurrentParent().FindForm();
                var dialog = Kernel.Container.Resolve<CommandEditorDialog>();
                dialog.ShowDialog(form);
            };
            debugMenu.DropDownItems.Add(menu);

            debugMenu.DropDownItems.Add(new ToolStripSeparator());

            menu = BuildMeasureMenu(debugMenu);
            debugMenu.DropDownItems.Add(menu);

            return debugMenu;
        }

        private ToolStripMenuItem BuildMeasureMenu(ToolStripMenuItem debug)
        {
            var menu = new ToolStripMenuItem("测量曲线窗体");
            menu.Click += async (s, e) =>
            {
                var form = debug.GetCurrentParent().FindForm();
                if (form != null && form is IWorkbench wb)
                {
                    var start = new SimpleMeasure();
                    start.Init();
                    
                    var solution = DUTSeriesStyleSolution.GetSolution(start.Pool);

                    _realTimeDataPlotView.SetSolution(solution);
                    _realTimeDataPlotView.Show(wb.MainDockPanel, DockState.Document);
                    await start.RunAsync(_workbenchViewModel);
                }
            };
            return menu;
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
