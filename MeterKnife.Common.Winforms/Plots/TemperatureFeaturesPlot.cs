using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Logging;
using MathNet.Numerics;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Util;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace MeterKnife.Common.Winforms.Plots
{
    public sealed class TemperatureFeaturesPlot : UserControl
    {
        private static readonly ILog _logger = LogManager.GetLogger<TemperatureFeaturesPlot>();

        private readonly LinearAxis _DataAxis = new LinearAxis();
        private readonly ScatterSeries _DataSeries = new ScatterSeries();

        private readonly PlotModel _PlotModel = new PlotModel();
        private readonly LineSeries _QuadraticCurveFittingSeries = new LineSeries();
        private readonly LineSeries _OnePolynomialSeries = new LineSeries();
        private readonly LinearAxis _TempAxis = new LinearAxis();

        public TemperatureFeaturesPlot()
        {
            Dock = DockStyle.Fill;
            var plot = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                Model = BuildPlotModel()
            };
            Controls.Add(plot);
        }

        private PlotModel BuildPlotModel()
        {
            _PlotModel.PlotAreaBackground = GetAreaColor();
            _PlotModel.Title = "温度特性图";

            _DataAxis.MaximumPadding = 0;
            _DataAxis.MinimumPadding = 0;
            _DataAxis.Maximum = 15;
            _DataAxis.Minimum = 5;
            _DataAxis.MinorGridlineStyle = LineStyle.Dot;
            _DataAxis.Position = AxisPosition.Left;
            _PlotModel.Axes.Add(_DataAxis);

            _TempAxis.MaximumPadding = 0;
            _TempAxis.MinimumPadding = 0;
            _TempAxis.MinorGridlineStyle = LineStyle.Dot;
            _TempAxis.Position = AxisPosition.Bottom;
            _PlotModel.Axes.Add(_TempAxis);

            return _PlotModel;
        }

        private bool _IsFrist = false;
        private void AddSeries()
        {
            if (_IsFrist)
                return;
            _DataSeries.MarkerFill = OxyColor.FromArgb(255, 0, 120, 0);
            _DataSeries.MarkerType = MarkerType.Diamond;
            _DataSeries.MarkerSize = 2.5;
            _PlotModel.Series.Add(_DataSeries);

            _QuadraticCurveFittingSeries.MarkerType = MarkerType.Circle;
            _QuadraticCurveFittingSeries.Smooth = true;
            _QuadraticCurveFittingSeries.Color = OxyColor.FromArgb(255, 255, 120, 0);
            _QuadraticCurveFittingSeries.MarkerFill = OxyColor.FromArgb(255, 255, 120, 0);
            _PlotModel.Series.Add(_QuadraticCurveFittingSeries);

            _OnePolynomialSeries.MarkerType = MarkerType.Circle;
            _OnePolynomialSeries.Smooth = true;
            _OnePolynomialSeries.Color = OxyColor.FromArgb(255, 160, 0, 160);
            _OnePolynomialSeries.MarkerFill = OxyColor.FromArgb(255, 160, 0, 160);
            _PlotModel.Series.Add(_OnePolynomialSeries);
            _IsFrist = true;
        }

        private void UpdateRange(FiguredData fd)
        {
            if (fd.ExtremePoint != null)
            {
                var max = fd.ExtremePoint.Item1;
                var min = fd.ExtremePoint.Item2;
                if (Math.Abs(max) > 0 && Math.Abs(min) > 0)
                {
                    var j = (Math.Abs(max - min))/6;
                    if (Math.Abs(j) > 0)
                    {
                        _DataAxis.Maximum = max + j;
                        _DataAxis.Minimum = min - j;
                    }
                }
            }
            if (fd.TemperatureExtremePoint != null)
            {
                var max = fd.TemperatureExtremePoint.Item1;
                var min = fd.TemperatureExtremePoint.Item2;
                if (Math.Abs(max) > 0 && Math.Abs(min) > 0)
                {
                    var j = (Math.Abs(max - min))/6;
                    if (Math.Abs(j) > 0)
                    {
                        _TempAxis.Maximum = max + j;
                        _TempAxis.Minimum = min - j;
                    }
                }
            }
        }

        private OxyColor GetAreaColor()
        {
            return OxyColor.FromArgb(255, 255, 245, 245);
        }

        public void Update(FiguredData fd)
        {
            AddSeries();
            UpdateRange(fd);

            var temps = new List<double>();
            var xarray = new List<double>();
            var yarray = new List<double>();
            foreach (DataRow row in fd.DataSet.Tables[1].Rows)
            {
                var x = (double) row[2]; //温度
                var y = (double) row[1]; //采集值
                if (Math.Abs(x) <= 0)
                    continue;
                var point = new ScatterPoint(x, y);
                _DataSeries.Points.Add(point);
                xarray.Add(x);
                yarray.Add(y);
                temps.Add(x);
            }

            if (temps.Count < 3)
                return;

            //******一次多项式*********************************************************************

            double m = 0, n = 0;
            // ReSharper disable once InconsistentNaming
            string polynomial_1 = string.Empty;//多项式

            try
            {
                double[] result;
                polynomial_1 = MathUtil.Polynomial1(xarray.ToArray(), yarray.ToArray(), out result);
                m = result[1];
                n = result[0];
                _logger.Info(polynomial_1);
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }

            //******二次多项式*********************************************************************

            double a = 0, b = 0, c = 0;
            // ReSharper disable once InconsistentNaming
            string polynomial_2 = string.Empty;//多项式

            try
            {
                double[] result;
                polynomial_2 = MathUtil.Polynomial2(xarray.ToArray(), yarray.ToArray(), out result);
                a = result[2];
                b = result[1];
                c = result[0];
                _logger.Info(polynomial_2);
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }

            temps.Sort();
            double t = 0;
            foreach (var temp in temps)
            {
                if (Math.Abs(temp - t) <= 0)
                    continue;
                t = temp;
                var two = a * temp * temp + b * temp + c;
                _QuadraticCurveFittingSeries.Points.Add(new DataPoint(temp, two));
                var one = m * temp + n;
                _OnePolynomialSeries.Points.Add(new DataPoint(temp, one));
            }
            _PlotModel.Subtitle = polynomial_2;
            _QuadraticCurveFittingSeries.Title = polynomial_2;
            _OnePolynomialSeries.Title = polynomial_1;
            this.ThreadSafeInvoke(() =>
            {
                _PlotModel.InvalidatePlot(true);
            });
        }

        public void Clear()
        {
            _PlotModel.Subtitle = string.Empty;
            _DataSeries.Points.Clear();
            _QuadraticCurveFittingSeries.Title = string.Empty;
            _QuadraticCurveFittingSeries.Points.Clear();
            _OnePolynomialSeries.Title = string.Empty;
            _OnePolynomialSeries.Points.Clear();
            this.ThreadSafeInvoke(() =>
            {
                _PlotModel.InvalidatePlot(true);
            });
        }
    }
}