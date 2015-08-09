using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace MeterKnife.Common.Controls.Plots
{
    public partial class TemperatureTrendPlot : UserControl
    {
        protected PlotModel _PlotModel = new PlotModel();
        protected LineSeries _DataSeries = new LineSeries();
        protected LineSeries _TempSeries = new LineSeries();
        protected LinearAxis _LeftAxis = new LinearAxis();
        protected LinearAxis _RightAxis = new LinearAxis();

        public TemperatureTrendPlot()
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
            _LeftAxis.MaximumPadding = 0;
            _LeftAxis.MinimumPadding = 0;
            _LeftAxis.Maximum = 15;
            _LeftAxis.Minimum = 5;
            _LeftAxis.Position = AxisPosition.Left;
            _PlotModel.Axes.Add(_LeftAxis);

            _RightAxis.MaximumPadding = 0;
            _RightAxis.MinimumPadding = 0;
            _RightAxis.Maximum = 15;
            _RightAxis.Minimum = 5;
            _RightAxis.Key = "temperature";
            _RightAxis.Position = AxisPosition.Right;
            _PlotModel.Axes.Add(_RightAxis);

            var timeAxis = new DateTimeAxis(); //时间刻度
            timeAxis.MajorGridlineStyle = LineStyle.Solid;
            timeAxis.MaximumPadding = 0;
            timeAxis.MinimumPadding = 0;
            timeAxis.MinorGridlineStyle = LineStyle.Dot;
            timeAxis.Position = AxisPosition.Bottom;
            _PlotModel.Axes.Add(timeAxis);

            _DataSeries.Color = OxyColor.FromArgb(255, 78, 154, 6);
            _DataSeries.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);

            _TempSeries.YAxisKey = "temperature";
            _TempSeries.Color = OxyColors.SlateBlue;
            _TempSeries.MarkerFill = OxyColors.SlateBlue;

            _PlotModel.Series.Add(_DataSeries);
            _PlotModel.Series.Add(_TempSeries);
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
                        _LeftAxis.Maximum = max + j;
                        _LeftAxis.Minimum = min - j;
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
                        _RightAxis.Maximum = max + j;
                        _RightAxis.Minimum = min - j;
                    }
                }
            }
        }

        public virtual void Update(FiguredData fd)
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

        public virtual void Clear()
        {
            _DataSeries.Points.Clear();
            _TempSeries.Points.Clear();
            this.ThreadSafeInvoke(() => _DataSeries.PlotModel.InvalidatePlot(true));
        }
    }
}