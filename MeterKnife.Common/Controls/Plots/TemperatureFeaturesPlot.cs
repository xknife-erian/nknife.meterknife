using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Logging;
using MathNet.Numerics;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace MeterKnife.Common.Controls.Plots
{
    public partial class TemperatureFeaturesPlot : UserControl
    {
        private static readonly ILog _logger = LogManager.GetLogger<TemperatureFeaturesPlot>();

        protected LinearAxis _DataAxis = new LinearAxis();
        protected ScatterSeries _DataSeries = new ScatterSeries();

        protected PlotModel _PlotModel = new PlotModel();
        protected LineSeries _QuadraticCurveFittingSeries = new LineSeries();
        protected LinearAxis _TempAxis = new LinearAxis();

        public TemperatureFeaturesPlot()
        {
            InitializeComponent();
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

            _DataSeries.MarkerFill = OxyColors.DarkRed;
            _DataSeries.MarkerType = MarkerType.Diamond;
            _DataSeries.MarkerSize = 2.5;
            _PlotModel.Series.Add(_DataSeries);

            _QuadraticCurveFittingSeries.MarkerType = MarkerType.Circle;
            _QuadraticCurveFittingSeries.Smooth = true;
            _QuadraticCurveFittingSeries.Color = OxyColor.FromArgb(255, 255, 100, 100);
            _QuadraticCurveFittingSeries.MarkerFill = OxyColor.FromArgb(255, 255, 100, 100);
            _PlotModel.Series.Add(_QuadraticCurveFittingSeries);

            return _PlotModel;
        }

        protected virtual void UpdateRange(FiguredData fd)
        {
            if (fd.ExtremePoint != null)
            {
                var max = fd.ExtremePoint.Item1;
                var min = fd.ExtremePoint.Item2;
                if (Math.Abs(max) > 0 && Math.Abs(min) > 0)
                {
                    var j = (Math.Abs(max - min))/4;
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
                    var j = (Math.Abs(max - min))/4;
                    if (Math.Abs(j) > 0)
                    {
                        _TempAxis.Maximum = max + j;
                        _TempAxis.Minimum = min - j;
                    }
                }
            }
        }

        public virtual void Update(FiguredData fd)
        {
            Clear();
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

            var v = Fit.Polynomial(xarray.ToArray(), yarray.ToArray(), 2);
            var a = v[2];
            var b = v[1];
            var c = v[0];

            var aa = a >= 0 ? "" : "-";
            var ab = b >= 0 ? "+" : "-";
            var bc = c >= 0 ? "+" : "-";
            var hs = string.Format("Y ={3} {0} X^2 {4} {1} X {5} {2}", Math.Abs(a).ToString("0.0000").TrimEnd('0'), Math.Abs(b).ToString("0.0000").TrimEnd('0'), Math.Abs(c).ToString("0.00000").TrimEnd('0'),aa, ab, bc);

            _logger.Info(a);
            _logger.Info(b);
            _logger.Info(c);
            _logger.Info(hs);

            temps.Sort();
            double t = 0;
            foreach (var temp in temps)
            {
                if (Math.Abs(temp - t) <= 0)
                    continue;
                t = temp;
                var ly = a*temp*temp + b*temp + c;
                _QuadraticCurveFittingSeries.Points.Add(new DataPoint(temp, ly));
                _logger.Trace(string.Format("Temp:{0}; Value:{1}", temp, ly));
            }
            _QuadraticCurveFittingSeries.Title = hs;
            this.ThreadSafeInvoke(() =>
            {
                _DataSeries.PlotModel.InvalidatePlot(true);
                _QuadraticCurveFittingSeries.PlotModel.InvalidatePlot(true);
            });
        }

        public virtual void Clear()
        {
            _DataSeries.Points.Clear();
            _QuadraticCurveFittingSeries.Points.Clear();
            this.ThreadSafeInvoke(() => _DataSeries.PlotModel.InvalidatePlot(true));
        }
    }
}