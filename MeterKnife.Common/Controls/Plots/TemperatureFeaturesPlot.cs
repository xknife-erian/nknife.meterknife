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
    public partial class TemperatureFeaturesPlot : UserControl
    {
        protected PlotModel _PlotModel = new PlotModel();
        protected ScatterSeries _Series = new ScatterSeries();

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
            _DataAxis.Position = AxisPosition.Left;
            _PlotModel.Axes.Add(_DataAxis);

            _TempAxis.MajorGridlineStyle = LineStyle.Solid;
            _TempAxis.MaximumPadding = 0;
            _TempAxis.MinimumPadding = 0;
            _TempAxis.MinorGridlineStyle = LineStyle.Dot;
            _TempAxis.Position = AxisPosition.Bottom;
            _PlotModel.Axes.Add(_TempAxis);

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
                    _DataAxis.Maximum = max + j;
                    _DataAxis.Minimum = min - j;
                }
            }
        }

        public virtual void Update(FiguredData fd)
        {
            UpdateRange(fd.Max.Output, fd.Min.Output);
            foreach (DataRow row in fd.DataSet.Tables[1].Rows)
            {
                var point = new DataPoint((double)row[2], (double)row[1]);
                //_Series.Points.Add(point);
            }
            this.ThreadSafeInvoke(() => _Series.PlotModel.InvalidatePlot(true));
        }

        public virtual void Clear()
        {
            _Series.Points.Clear();
            this.ThreadSafeInvoke(() => _Series.PlotModel.InvalidatePlot(true));
        }
    }
}
