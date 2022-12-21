using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.ViewModels.Plots;
using NLog;
using OxyPlot;
using OxyPlot.Axes;

namespace NKnife.MeterKnife.ViewModels
{
    public class StaticDataPlotViewModel : PlotViewModel
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly IProjectLogic _projectLogic;
        private Project _project;

        public StaticDataPlotViewModel(IHabitManager habit, IProjectLogic projectLogic) 
            : base(habit)
        {
            _projectLogic = projectLogic;
        }

        public void SetEngineering(Project project)
        {
            _project = project;
            var duts = _project.GetIncludedDUTArray();
            SetStyleSolutionByEngineering(duts);
        }

        public async Task LoadDataAsync()
        {
            //LinearPlot.ClearValues();
            var duts = _project.GetIncludedDUTArray();
            // TODO:是否可以转为并行计算
            for (int i = 0; i < duts.Count; i++)
            {
                var r = await _projectLogic.GetDUTDataAsync((_project, duts[i]));
                LinearPlot.AddValues(i, r.ToArray());
            }
        }

        private void SetStyleSolutionByEngineering(List<DUT> duts)
        {
            var left = 0;
            var right = 0;
            var solution = new DUTSeriesStyleSolution();
            for (var index = 0; index < duts.Count; index++)
            {
                var style = DUTSeriesStyle.Build(LineStyle.Solid); //.GetAllLineStyles()[index];
                style.Color = PlotTheme.CommonlyUsedColors[index];
                style.DUT = duts[index].Id;

                style.Axis = new LinearAxis();
                style.Axis.Key = duts[index].Id;
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

            StyleSolution = solution;
        }
    }
}