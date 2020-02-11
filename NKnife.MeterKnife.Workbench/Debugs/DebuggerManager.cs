using System;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.MeterKnife.Workbench.Views;
using NKnife.Util;
using NKnife.Win.Quick.Base;
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
            var debug = new ToolStripMenuItem("Debug");
            var plot = new ToolStripMenuItem("Plot");
            plot.Click += async (s, e) =>
            {
                var form = debug.GetCurrentParent().FindForm();
                if (form != null && form is IWorkbench wb)
                {
                    var start = new SimpleMeasure();
                    start.Init(_antService, _connector);
                        var solution = new PlotSeriesStyleSolution();
                    foreach (var cmd in start.Pool)
                    {
                        solution.Add(new DUTSeriesStyle(cmd.DUT.Id, new SeriesStyle()));
                    }

                    _measureView.ViewModel.SeriesStyleSolution = solution;
                    _measureView.Show(wb.MainDockPanel, DockState.Document);
                    await start.RunAsync(_antService, _engineeringLogic);
                }
            };
            debug.DropDownItems.Add(plot);
            return debug;
        }

        public static ToolStripMenuItem GetDebugItem()
        {
            var word = Guid.NewGuid().ToString("N").ToUpper();
            var t = new ToolStripMenuItem($"{word.Substring(0, UtilRandom.Next(4, 8))}(&{word[0]})");
            t.Click += (s, e) => { MessageBox.Show(word); };
            return t;
        }
    }
}
