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
        private readonly LinearAxis _LeftAxis2 = new LinearAxis();
        private readonly PlotModel _PlotModel2 = new PlotModel();
        private readonly LineSeries _Series2 = new LineSeries();

        private readonly LinearAxis _LeftAxis3 = new LinearAxis();
        private readonly PlotModel _PlotModel3 = new PlotModel();
        private readonly LineSeries _Series3 = new LineSeries();

        private Random _Random = new Random((int) DateTime.Now.Ticks);

        public OxyPlotTestForm()
        {
            InitializeComponent();
            var plot1 = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                Model = BuildPlotModel()
            };
            var plot2 = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                Model = BuildPlotModel2()
            };
            splitContainer1.Panel1.Controls.Add(plot1);
            splitContainer1.Panel2.Controls.Add(plot2);
            propertyGrid1.SelectedObject = _PlotModel2;
        }

        private PlotModel BuildPlotModel()
        {
            _PlotModel2.Updated += _PlotModel_Updated;
            _PlotModel2.PlotAreaBackground = OxyColors.LightSeaGreen;

            _LeftAxis2.MajorGridlineStyle = OxyPlot.LineStyle.Solid;
            _LeftAxis2.MinorGridlineStyle = OxyPlot.LineStyle.Dot;
            _LeftAxis2.MaximumPadding = 0;
            _LeftAxis2.MinimumPadding = 0;
            _LeftAxis2.Maximum = 15;
            _LeftAxis2.Minimum = 5;
            _LeftAxis2.Position = AxisPosition.Left;
            _PlotModel2.Axes.Add(_LeftAxis2);

            var timeAxis2 = new DateTimeAxis(); //时间刻度
            timeAxis2.MajorGridlineStyle = OxyPlot.LineStyle.Solid;
            timeAxis2.MinorGridlineStyle = OxyPlot.LineStyle.Dot;
            timeAxis2.MaximumPadding = 0;
            timeAxis2.MinimumPadding = 0;
            timeAxis2.Position = AxisPosition.Bottom;
            timeAxis2.Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-1));
            _PlotModel2.Axes.Add(timeAxis2);

            _Series2.Color = OxyColor.FromArgb(255, 78, 154, 6);
            _Series2.MarkerFill = OxyColor.FromArgb(255, 24, 45, 6);//(255, 78, 154, 6);
            _Series2.MarkerStroke = OxyColors.ForestGreen;
            _Series2.StrokeThickness = 3;

            _Series2.TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}";
            _PlotModel2.Series.Add(_Series2);
            return _PlotModel2;
        }

        private PlotModel BuildPlotModel2()
        {

            _PlotModel3.PlotAreaBackground = OxyColors.CadetBlue;

            _LeftAxis3.MajorGridlineStyle = OxyPlot.LineStyle.Solid;
            _LeftAxis3.MinorGridlineStyle = OxyPlot.LineStyle.Dot;
            _LeftAxis3.MaximumPadding = 0;
            _LeftAxis3.MinimumPadding = 0;
            _LeftAxis3.Maximum = 15;
            _LeftAxis3.Minimum = 5;
            _LeftAxis3.Position = AxisPosition.Left;
            _PlotModel3.Axes.Add(_LeftAxis3);

            var timeAxis1 = new DateTimeAxis(); //时间刻度
            timeAxis1.MajorGridlineStyle = OxyPlot.LineStyle.Solid;
            timeAxis1.MinorGridlineStyle = OxyPlot.LineStyle.Dot;
            timeAxis1.MaximumPadding = 0;
            timeAxis1.MinimumPadding = 0;
            timeAxis1.Position = AxisPosition.Bottom;
            timeAxis1.Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-1));
            _PlotModel3.Axes.Add(timeAxis1);

            _Series3.Color = OxyColor.FromArgb(255, 78, 154, 6);
            _Series3.MarkerFill = OxyColor.FromArgb(255, 24, 45, 6);//(255, 78, 154, 6);
            _Series3.MarkerStroke = OxyColors.ForestGreen;
            _Series3.StrokeThickness = 3;

            _Series3.TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}";
            _PlotModel3.Series.Add(_Series3);

            return _PlotModel3;
        }

        void _PlotModel_Updated(object sender, EventArgs e)
        {
            _StatusLeftLabel.Text = _PlotModel2.PlotMargins.Left.ToString();
        }

        private double _Left1 = 0;
        private double _Left2 = 0;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _Left1 = _Random.Next(1, 999);
            _Left2 = _Random.Next(1, 999);

            DataPoint dp1 = new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), _Left1);
            _Series2.Points.Add(dp1);

            DataPoint dp2 = new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), _Left2);
            _Series3.Points.Add(dp2);

            _PlotModel2.InvalidatePlot(true);
            propertyGrid1.Refresh();
        }
    }
}
