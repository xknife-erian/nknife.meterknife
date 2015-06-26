using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace MeterKnife.Common.Controls.Plots
{
    public abstract partial class DataPlot : UserControl
    {
        protected PlotModel _PlotModel = new PlotModel();
        protected LineSeries _Series = new LineSeries();
        protected LinearAxis _LeftAxis = new LinearAxis();

        protected DataPlot()
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

        protected virtual PlotModel BuildPlotModel()
        {
            _LeftAxis.MaximumPadding = 0;
            _LeftAxis.MinimumPadding = 0;
            _LeftAxis.Maximum = 15;
            _LeftAxis.Minimum = 5;
            _LeftAxis.Position = AxisPosition.Left;
            _PlotModel.Axes.Add(_LeftAxis);

            var timeAxis = new DateTimeAxis(); //时间刻度
            timeAxis.MajorGridlineStyle = LineStyle.Solid;
            timeAxis.MaximumPadding = 0;
            timeAxis.MinimumPadding = 0;
            timeAxis.MinorGridlineStyle = LineStyle.Dot;
            timeAxis.Position = AxisPosition.Bottom;
            _PlotModel.Axes.Add(timeAxis);

            _Series.Color = OxyColor.FromArgb(255, 78, 154, 6);
            _Series.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);
            _PlotModel.Series.Add(_Series);
            return _PlotModel;
        }

        protected virtual void UpdateRange(double max, double min)
        {
            if (Math.Abs(max) > 0 && Math.Abs(min) > 0)
            {
                double j = (Math.Abs(max - min)) / 4;
                if (Math.Abs(j) > 0)
                {
                    _LeftAxis.Maximum = max + j;
                    _LeftAxis.Minimum = min - j;
                }
            }
        }

        protected object SelectValue(FiguredData fd)
        {
            return fd.DataSet.Tables[1].Rows[fd.DataSet.Tables[1].Rows.Count][ValueHead];
        }

        public virtual void Update(FiguredData fd)
        {
            var yzl = SelectValue(fd);
            var value = double.Parse(yzl.ToString());

            _PlotModel.Title = value.ToString();

            var dataPoint = DateTimeAxis.CreateDataPoint(DateTime.Now, value);
            _Series.Points.Add(dataPoint);
            _Series.PlotModel.InvalidatePlot(true);

            UpdateRange(fd.Max.Output, fd.Min.Output);
        }

        public abstract string ValueHead { get; }

    }
}