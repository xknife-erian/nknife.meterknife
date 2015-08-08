using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    public sealed class StandardNormalDistributionPlot : UserControl
    {
        private CategoryAxis _SndAxis = new CategoryAxis();
        private PlotModel _PlotModel = new PlotModel();

        public StandardNormalDistributionPlot()
        {
            SuspendLayout();
            Dock = DockStyle.Fill;
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            var plot = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                Model = _PlotModel
            };
            Controls.Add(plot);
            ResumeLayout(false);
        }

        private PlotModel BuildPlotModel()
        {
            //_SndAxis.GapWidth = 0;
            _SndAxis.IsAxisVisible = false;
            //_SndAxis.MinorStep = 1;
            _SndAxis.Position = AxisPosition.Left;
            _PlotModel.Axes.Add(_SndAxis);
            return _PlotModel;
        }

        public void Update(FiguredData fd)
        {
            var categoryAxis1 = new CategoryAxis();
            categoryAxis1.GapWidth = 0;
            categoryAxis1.IsAxisVisible = false;
            categoryAxis1.MinorStep = 1;

            DataTable table = fd.DataSet.Tables[1];
            int count = fd.DataSet.Tables[1].Rows.Count;
            if (count < 3) return;
            for (int i = 0; i < count; i++)
            {
                var sd = (double)(table.Rows[i]["standard_deviation"]);
                categoryAxis1.ActualLabels.Add(sd.ToString("f8"));
            }
             _PlotModel.Axes.Add(categoryAxis1);

            var linearAxis1 = new LinearAxis();
            linearAxis1.Maximum = 40000;
            linearAxis1.Minimum = -40000;
            linearAxis1.Position = AxisPosition.Bottom;
            _PlotModel.Axes.Add(linearAxis1);

            var linearAxis2 = new LinearAxis();
            linearAxis2.AbsoluteMinimum = 0;
            linearAxis2.MinimumPadding = 0;
            _PlotModel.Axes.Add(linearAxis2);

            var barSeries1 = new ColumnSeries();
            barSeries1.ValueField = "Value";
            _PlotModel.Series.Add(barSeries1);

            barSeries1.PlotModel.InvalidatePlot(true);
        }

        public void Clear()
        {
            this.ThreadSafeInvoke(() => _SndAxis.Labels.Clear());
        }
    }
}