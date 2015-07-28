﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Axes;

namespace MeterKnife.Common.Controls.Plots
{
    public class TemperatureDataPlot : DataPlot
    {
        public override string ValueHead
        {
            get { return "temperature"; }
        }

        protected override OxyColor GetColor()
        {
            return OxyColor.FromArgb(255, 86, 96, 225);
        }

        protected override void UpdateRange(FiguredData fd)
        {
            var max = fd.TemperatureMax.Output;
            var min = fd.TemperatureMin.Output;
            UpdateRange(max, min);
        }

        public override void Update(FiguredData fd)
        {
            var yzl = SelectValue(fd);
            var value = double.Parse(yzl.ToString());

            _PlotModel.Title = value.ToString();

            var dataPoint = DateTimeAxis.CreateDataPoint(DateTime.Now, value);
            _Series.Points.Add(dataPoint);
            _Series.PlotModel.InvalidatePlot(true);

            UpdateRange(fd);
        }
    }
}
