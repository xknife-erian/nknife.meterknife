﻿using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.ViewModels.Plots;

namespace NKnife.MeterKnife.ViewModels
{
    public abstract class PlotViewModel : ObservableRecipient
    {
        protected DUTSeriesStyleSolution _solution = new DUTSeriesStyleSolution();

        protected PlotViewModel(IHabitManager habit)
        {
            var dvList = new List<PlotTheme>();
            var dv = new PlotTheme();
            dvList.Add(dv);
            var themes = habit.GetHabitValue(HabitKey.Plot_PlotThemes, dvList);
            var usingTheme = habit.GetHabitValue(HabitKey.Plot_UsingTheme, dv.Name);
            foreach (var plotTheme in themes)
            {
                if (plotTheme.Name == usingTheme)
                {
                    var ySpaceLevel = habit.GetOptionValue(HabitKey.Plot_YSpace, (short)5);
                    LinearPlot = new DUTLinearPlot(plotTheme, ySpaceLevel);
                }
            }
        }

        public DUTSeriesStyleSolution StyleSolution
        {
            get => _solution;
            set
            {
                SetProperty(ref _solution, value);
                LinearPlot.SetSeries(value.ToArray());
            }
        }

        public DUTLinearPlot LinearPlot { get; }

        public event EventHandler PlotModelUpdated;

        protected virtual void OnPlotModelUpdated()
        {
            PlotModelUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}