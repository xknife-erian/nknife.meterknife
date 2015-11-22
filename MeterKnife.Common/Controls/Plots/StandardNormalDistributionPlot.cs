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
    /// <summary>
    /// 标准正态分布
    /// </summary>
    public sealed class StandardNormalDistributionPlot : UserControl
    {
        private readonly PlotModel _PlotModel = new PlotModel();

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

//        private PlotModel BuildPlotModel()
//        {
//            //_SndAxis.GapWidth = 0;
//            _SndAxis.IsAxisVisible = false;
//            //_SndAxis.MinorStep = 1;
//            _SndAxis.Position = AxisPosition.Left;
//            _PlotModel.Axes.Add(_SndAxis);
//            return _PlotModel;
//        }

        public void Update(FiguredData fd)
        {
            DataTable table = fd.DataSet.Tables[1];
            int count = fd.DataSet.Tables[1].Rows.Count;
            if (count < 3) return;

            var categoryAxis = new CategoryAxis();
            categoryAxis.GapWidth = 0;//分类间距
            categoryAxis.IsAxisVisible = false;
            //categoryAxis.MinorStep = 1;
            for (int i = 0; i < count; i++)
            {
                var sd = (double) (table.Rows[i][FiguredData.STANDARD_DEVIATION]);
                var label = sd.ToString("f8");
                categoryAxis.Labels.Add(label);
                categoryAxis.ActualLabels.Add(label);
            }
            _PlotModel.Axes.Add(categoryAxis);

            var linearAxis1 = new LinearAxis();
            linearAxis1.Maximum = fd.ExtremePoint.Item1;
            linearAxis1.Minimum = fd.ExtremePoint.Item2;
            linearAxis1.Position = AxisPosition.Bottom;
            _PlotModel.Axes.Add(linearAxis1);

            var linearAxis2 = new LinearAxis();
            linearAxis2.AbsoluteMinimum = 0;
            linearAxis2.MinimumPadding = 0;
            _PlotModel.Axes.Add(linearAxis2);

            var columnSeries = new ColumnSeries();
            columnSeries.ValueField = "Value";
            _PlotModel.Series.Add(columnSeries);

            this.ThreadSafeInvoke(() => _PlotModel.InvalidatePlot(true));
        }

        public void Clear()
        {
            this.ThreadSafeInvoke(() =>
            {
                _PlotModel.Axes.Clear();
                _PlotModel.Series.Clear();
            });
        }
    }
}