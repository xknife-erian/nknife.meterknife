using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace MeterKnife.Common.Winforms.Plots
{
    public abstract partial class DataPlot : UserControl
    {
        protected LinearAxis _LeftAxis = new LinearAxis();
        private double _Max;
        private double _Min;
        protected PlotModel _PlotModel = new PlotModel();
        protected LineSeries _Series = new LineSeries();
        protected DateTimeAxis _TimeAxis = new DateTimeAxis();

        protected DataPlot()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Dock = DockStyle.Fill;
            var plot = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                // ReSharper disable once DoNotCallOverridableMethodsInConstructor
                Model = BuildPlotModel()
            };
            Controls.Add(plot);
        }

        public abstract string ValueHead { get; }

        protected virtual PlotModel BuildPlotModel()
        {
            _PlotModel.PlotAreaBackground = GetAreaColor();

            _LeftAxis.MajorGridlineColor = OxyColor.FromArgb(25, 0, 0, 90);
            _LeftAxis.MinorGridlineColor = OxyColor.FromArgb(15, 0, 0, 90);
            _LeftAxis.MajorGridlineStyle = LineStyle.Solid;
            _LeftAxis.MinorGridlineStyle = LineStyle.Dot;
            _LeftAxis.MaximumPadding = 0;
            _LeftAxis.MinimumPadding = 0;
            _LeftAxis.Angle = GetLeftAxisAngle();
            _LeftAxis.Maximum = 15;
            _LeftAxis.Minimum = 5;
            _LeftAxis.Position = AxisPosition.Left;
            _PlotModel.Axes.Add(_LeftAxis);

            _TimeAxis.MajorGridlineColor = OxyColor.FromArgb(25, 0, 0, 90);
            _TimeAxis.MinorGridlineColor = OxyColor.FromArgb(15, 0, 0, 90);
            _TimeAxis.MajorGridlineStyle = LineStyle.Solid;
            _TimeAxis.MinorGridlineStyle = LineStyle.Dot;
            _TimeAxis.MaximumPadding = 0;
            _TimeAxis.MinimumPadding = 0;
            _TimeAxis.Position = AxisPosition.Bottom;
            _TimeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");
            _PlotModel.Axes.Add(_TimeAxis);

            _Series.Color = GetMainSeriesColor();
            _Series.MarkerFill = OxyColor.FromArgb(255,24,45,6);//(255, 78, 154, 6);
            _Series.MarkerStroke = OxyColors.ForestGreen;
            _Series.StrokeThickness = GetThickness();

            _Series.TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}";
            _PlotModel.Series.Add(_Series);
            return _PlotModel;
        }

        protected virtual double GetLeftAxisAngle()
        {
            return 45;
        }

        protected virtual OxyColor GetMainSeriesColor()
        {
            return OxyColor.FromArgb(255, 78, 154, 6);
        }

        protected virtual OxyColor GetAreaColor()
        {
            return OxyColor.FromArgb(255, 245, 255, 245);
        }

        protected virtual double GetThickness()
        {
            return 2;
        }

        protected virtual void UpdateRange(FiguredData fd)
        {
            double max = fd.ExtremePoint.Item1;
            double min = fd.ExtremePoint.Item2;
            UpdateRange(max, min);
        }

        protected virtual void UpdateRange(double max, double min)
        {
            if (_Max == max && _Min == min && (_Max != 0 || _Min != 0 || max != 0 || min != 0))
                return;
            _Max = max;
            _Min = min;
            if (Math.Abs(max - min) <= 0)
            {
                _LeftAxis.Maximum = max + 0.01;
                _LeftAxis.Minimum = min - 0.01;
            }
            else if (Math.Abs(max) > 0 && Math.Abs(min) > 0)
            {
                double j = (Math.Abs(max - min))/6;
                if (Math.Abs(j) > 0)
                {
                    _LeftAxis.Maximum = max + j;
                    _LeftAxis.Minimum = min - j;
                }
            }
        }

        protected object SelectValue(FiguredData fd)
        {
            var row = fd.DataSet.Tables[1].AsEnumerable().Last();
            return row == null ? 0 : row[ValueHead];
        }

        public virtual void Update(FiguredData fd)
        {
            object yzl = SelectValue(fd);
            double value = double.Parse(yzl.ToString());

            _PlotModel.Title = value.ToString();

            DataPoint dataPoint = DateTimeAxis.CreateDataPoint(DateTime.Now, value);
            _Series.Points.Add(dataPoint);
            this.ThreadSafeInvoke(() => _Series.PlotModel.InvalidatePlot(true));

            UpdateRange(fd);
        }

        public virtual void Clear()
        {
            _PlotModel.Title = " ";
            _Series.Points.Clear();
            this.ThreadSafeInvoke(() => _Series.PlotModel.InvalidatePlot(true));
        }
    }
}