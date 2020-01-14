using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace MeterKnife.Common.Winforms.Plots
{
    /// <summary>
    /// 温度趋势图: 温度曲线与数据曲线叠加
    /// </summary>
    public sealed class TemperatureTrendPlot : UserControl
    {
        readonly PlotModel _PlotModel = new PlotModel();
        readonly LineSeries _DataSeries = new LineSeries();
        readonly LinearAxis _DataAxis = new LinearAxis();
        readonly LineSeries _TempSeries = new LineSeries();
        readonly LinearAxis _TempAxis = new LinearAxis();
        readonly DateTimeAxis _TimeAxis = new DateTimeAxis(); //时间刻度

        public TemperatureTrendPlot()
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

            _TimeAxis.MajorGridlineStyle = LineStyle.Solid;
            _TimeAxis.MinorGridlineStyle = LineStyle.Dot;
            _TimeAxis.MaximumPadding = 0;
            _TimeAxis.MinimumPadding = 0;
            _TimeAxis.Position = AxisPosition.Bottom;
            _TimeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");
            _PlotModel.Axes.Add(_TimeAxis);

            _DataAxis.MaximumPadding = 0;
            _DataAxis.MinimumPadding = 0;
            _DataAxis.Maximum = 15;
            _DataAxis.Minimum = 5;
            _DataAxis.Position = AxisPosition.Left;
            _PlotModel.Axes.Add(_DataAxis);

            _TempAxis.MaximumPadding = 0;
            _TempAxis.MinimumPadding = 0;
            _TempAxis.Maximum = 15;
            _TempAxis.Minimum = 5;
            _TempAxis.Key = "temperature";
            _TempAxis.Position = AxisPosition.Right;
            _PlotModel.Axes.Add(_TempAxis);

            _DataSeries.Color = OxyColor.FromArgb(255, 78, 154, 6);
            _DataSeries.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);
            _DataSeries.StrokeThickness = 2.7;

            _TempSeries.YAxisKey = "temperature";
            _TempSeries.Color = OxyColors.Red;
            _TempSeries.MarkerFill = OxyColors.Red;
            _TempSeries.StrokeThickness = 1.5;

            _PlotModel.Series.Add(_TempSeries);
            _PlotModel.Series.Add(_DataSeries);
            return _PlotModel;
        }

        private OxyColor GetAreaColor()
        {
            return OxyColor.FromArgb(255, 245, 255, 255);
        }

        private void UpdateRange(FiguredData fd)
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

        public void Update(FiguredData fd)
        {
            Clear();
            UpdateRange(fd);
            foreach (DataRow row in fd.DataSet.Tables[1].Rows)
            {
                var time = (DateTime) row[0];
                var left = (double)row[1];
                var right = (double)row[2];
                if (Math.Abs(right) <= 0)
                    continue;
                var point = DateTimeAxis.CreateDataPoint(time, left); //new ScatterPoint(x, y);
                _DataSeries.Points.Add(point);
                point = DateTimeAxis.CreateDataPoint(time, right); //new ScatterPoint(x, y);
                _TempSeries.Points.Add(point);
            }
            this.ThreadSafeInvoke(() =>
            {
                _DataSeries.PlotModel.InvalidatePlot(true);
                _TempSeries.PlotModel.InvalidatePlot(true);
            });
        }

        public void Clear()
        {
            _DataSeries.Points.Clear();
            _TempSeries.Points.Clear();
            this.ThreadSafeInvoke(() => _DataSeries.PlotModel.InvalidatePlot(true));
        }
    }
}