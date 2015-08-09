using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.DataModels;
using NPOI.HSSF.Record.Chart;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using MarkerType = OxyPlot.MarkerType;

namespace MeterKnife.Common.Controls.Plots
{
    public partial class TemperatureFeaturesPlot : UserControl
    {
        private static readonly ILog _logger = LogManager.GetLogger<TemperatureFeaturesPlot>();

        protected PlotModel _PlotModel = new PlotModel();
        protected ScatterSeries _DataSeries = new ScatterSeries();
        protected LineSeries _QuadraticCurveFittingSeries = new LineSeries();

        protected LinearAxis _DataAxis = new LinearAxis();
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
                    double j = (Math.Abs(max - min))/4;
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
                    double j = (Math.Abs(max - min))/4;
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
            var lsqr = new LstSquQuadRegr();
            foreach (DataRow row in fd.DataSet.Tables[1].Rows)
            {
                var x = (double) row[2]; //温度
                var y = (double) row[1]; //采集值
                if (Math.Abs(x) <= 0)
                    continue;
                var point = new ScatterPoint(x, y);
                _DataSeries.Points.Add(point);
                lsqr.AddPoints(x, y);
                temps.Add(x);
            }

            if (temps.Count < 3)
                return;

            var a = lsqr.aTerm();
            var b = lsqr.bTerm();
            var c = lsqr.cTerm();

            temps.Sort();
            _logger.Info(string.Format("y={0} x*x + {1} x + {2}", a, b, c));
            double t = 0;
            foreach (var temp in temps)
            {
                if (Math.Abs(temp - t) <= 0)
                    continue;
                t = temp;
                var ly = a*temp*temp + b*temp + c;
                _QuadraticCurveFittingSeries.Points.Add(new DataPoint(temp, ly));
                _logger.Info(string.Format("Temp:{0}; Value:{1}", temp, ly));
            }
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
