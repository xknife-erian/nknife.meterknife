using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.GUI.WinForm;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using LineStyle = NKnife.GUI.WinForm.LineStyle;

namespace MeterKnife.DemoApplication
{
    public sealed partial class OxyPlotTestForm : SimpleForm
    {
        private readonly LinearAxis _LeftAxis = new LinearAxis();
        private readonly PlotModel _PlotModel = new PlotModel();
        private readonly LineSeries _Series = new LineSeries();

        public OxyPlotTestForm()
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
            _SplitContainer.Panel1.Controls.Add(plot);
        }

        private PlotModel BuildPlotModel()
        {
            _PlotModel.PlotAreaBackground = OxyColors.LightSeaGreen;

            _LeftAxis.MajorGridlineStyle = OxyPlot.LineStyle.Solid;
            _LeftAxis.MinorGridlineStyle = OxyPlot.LineStyle.Dot;
            _LeftAxis.MaximumPadding = 0;
            _LeftAxis.MinimumPadding = 0;
            _LeftAxis.Maximum = 15;
            _LeftAxis.Minimum = 5;
            _LeftAxis.Position = AxisPosition.Left;
            _PlotModel.Axes.Add(_LeftAxis);

            var timeAxis = new DateTimeAxis(); //时间刻度
            timeAxis.MajorGridlineStyle = OxyPlot.LineStyle.Solid;
            timeAxis.MinorGridlineStyle = OxyPlot.LineStyle.Dot;
            timeAxis.MaximumPadding = 0;
            timeAxis.MinimumPadding = 0;
            timeAxis.Position = AxisPosition.Bottom;
            timeAxis.Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-1));
            _PlotModel.Axes.Add(timeAxis);

            _Series.Color = OxyColor.FromArgb(255, 78, 154, 6);
            _Series.MarkerFill = OxyColor.FromArgb(255, 24, 45, 6);//(255, 78, 154, 6);
            _Series.MarkerStroke = OxyColors.ForestGreen;
            _Series.StrokeThickness = 3;

            _Series.TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}";
            _PlotModel.Series.Add(_Series);
            return _PlotModel;
        }
    }
}
