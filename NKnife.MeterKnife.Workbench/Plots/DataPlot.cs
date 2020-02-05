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

namespace NKnife.MeterKnife.Workbench.Plots
{
    public abstract class DataPlot : UserControl
    {
        protected LinearAxis _DataAxis = new LinearAxis();
        private double _Max;
        private double _Min;
        protected PlotModel _DataPlotModel = new PlotModel();
        protected LineSeries _DataSeries = new LineSeries();
        protected DateTimeAxis _TimeAxis = new DateTimeAxis();

        protected PlotView _Plot = new PlotView();

        protected DataPlot()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Dock = DockStyle.Fill;
            _Plot.Dock = DockStyle.Fill;
            _Plot.PanCursor = Cursors.Hand;
            _Plot.BackColor = Color.White;
            _Plot.ZoomHorizontalCursor = Cursors.SizeWE;
            _Plot.ZoomRectangleCursor = Cursors.SizeNWSE;
            _Plot.ZoomVerticalCursor = Cursors.SizeNS;
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            _Plot.Model = BuildPlotModel();
            Controls.Add(_Plot);
        }

        public abstract string ValueHead { get; }

        protected virtual PlotModel BuildPlotModel()
        {
            _DataPlotModel.Updating += _PlotModel_Updating;
            _DataPlotModel.Updated += _PlotModel_Updated;
            _DataPlotModel.PlotAreaBackground = GetAreaColor();
            _DataPlotModel.Title = GetPlotTitle();

            _DataAxis.MajorGridlineColor = OxyColor.FromArgb(25, 0, 0, 90);
            _DataAxis.MinorGridlineColor = OxyColor.FromArgb(15, 0, 0, 90);
            _DataAxis.MajorGridlineStyle = LineStyle.Solid;
            _DataAxis.MinorGridlineStyle = LineStyle.Dot;
            _DataAxis.MaximumPadding = 0;
            _DataAxis.MinimumPadding = 0;
            _DataAxis.Angle = GetLeftAxisAngle();
            _DataAxis.Maximum = 15;
            _DataAxis.Minimum = 5;
            _DataAxis.Position = AxisPosition.Left;
            _DataPlotModel.Axes.Add(_DataAxis);

            _TimeAxis.MajorGridlineColor = OxyColor.FromArgb(25, 0, 0, 90);
            _TimeAxis.MinorGridlineColor = OxyColor.FromArgb(15, 0, 0, 90);
            _TimeAxis.MajorGridlineStyle = LineStyle.Solid;
            _TimeAxis.MinorGridlineStyle = LineStyle.Dot;
            _TimeAxis.MaximumPadding = 0;
            _TimeAxis.MinimumPadding = 0;
            _TimeAxis.Position = AxisPosition.Bottom;
            _TimeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");
            _DataPlotModel.Axes.Add(_TimeAxis);

            _DataSeries.Color = GetMainSeriesColor();
            _DataSeries.MarkerFill = OxyColor.FromArgb(255, 24, 45, 6); //(255, 78, 154, 6);
            _DataSeries.MarkerStroke = OxyColors.ForestGreen;
            _DataSeries.StrokeThickness = GetThickness();

            _DataSeries.TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}";
            _DataPlotModel.Series.Add(_DataSeries);
            return _DataPlotModel;
        }

        public OxyThickness PlotMargin
        {
            get { return _DataPlotModel.PlotMargins; }
            set { _DataPlotModel.PlotMargins = value; }
        }

        public OxyRect PlotArea
        {
            get { return _DataPlotModel.PlotArea; }
        }

        protected virtual string GetPlotTitle()
        {
            return " ";
        }

        protected virtual double GetLeftAxisAngle()
        {
            return 0;
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
            var max = fd.ExtremePoint.Item1;
            var min = fd.ExtremePoint.Item2;
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
                _DataAxis.Maximum = max + 0.01;
                _DataAxis.Minimum = min - 0.01;
            }
            else if (Math.Abs(max) > 0 && Math.Abs(min) > 0)
            {
                var j = (Math.Abs(max - min))/6;
                if (Math.Abs(j) > 0)
                {
                    _DataAxis.Maximum = max + j;
                    _DataAxis.Minimum = min - j;
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
            var yzl = SelectValue(fd);
            var value = double.Parse(yzl.ToString());

            _DataPlotModel.Title = value.ToString();

            var dataPoint = DateTimeAxis.CreateDataPoint(DateTime.Now, value);
            _DataSeries.Points.Add(dataPoint);
            this.ThreadSafeInvoke(() => _DataSeries.PlotModel.InvalidatePlot(true));

            UpdateRange(fd);
        }

        public virtual void Clear()
        {
            _DataPlotModel.Title = " ";
            _DataSeries.Points.Clear();
            this.ThreadSafeInvoke(() => _DataSeries.PlotModel.InvalidatePlot(true));
        }

        #region PlotModel Event

        protected void _PlotModel_Updating(object sender, EventArgs e)
        {
            OnPlotUpdating(sender, e);
        }

        protected void _PlotModel_Updated(object sender, EventArgs e)
        {
            OnPlotUpdated(sender, e);
        }

        public event EventHandler PlotUpdating;

        protected virtual void OnPlotUpdating(object sender, EventArgs e)
        {
            var handler = PlotUpdating;
            if (handler != null) handler(sender, e);
        }

        public event EventHandler PlotUpdated;

        protected virtual void OnPlotUpdated(object sender, EventArgs e)
        {
            var handler = PlotUpdated;
            if (handler != null) handler(sender, e);
        }

        #endregion
    }
}