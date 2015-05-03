using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Instruments
{
    public partial class CollectDataView : DockContent
    {
        protected PlotView _MainPlot = new PlotView();
        protected PlotView _TempPlot = new PlotView();

        public CollectDataView()
        {
            InitializeComponent();

            _MainPlot.Dock = DockStyle.Fill;
            _MainPlot.PanCursor = Cursors.Hand;
            _MainPlot.BackColor = Color.White;
            _MainPlot.ZoomHorizontalCursor = Cursors.SizeWE;
            _MainPlot.ZoomRectangleCursor = Cursors.SizeNWSE;
            _MainPlot.ZoomVerticalCursor = Cursors.SizeNS;
            _PlotSplitContainer.Panel1.Controls.Add(_MainPlot);

            _TempPlot.Dock = DockStyle.Fill;
            _TempPlot.PanCursor = Cursors.Hand;
            _TempPlot.BackColor = Color.White;
            _TempPlot.ZoomHorizontalCursor = Cursors.SizeWE;
            _TempPlot.ZoomRectangleCursor = Cursors.SizeNWSE;
            _TempPlot.ZoomVerticalCursor = Cursors.SizeNS;
            _PlotSplitContainer.Panel2.Controls.Add(_TempPlot);
        }

        private IMeter _Meter;

        public IMeter Meter
        {
            get
            {
                return _Meter;
            }
            set
            {
                _Meter = value;
                _FiguredDataPropertyGrid.SelectedObject = new FiguredData();
                _MeterParamPropertyGrid.SelectedObject = Meter.Parameters;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var myModel1 = new PlotModel { Title = "电压" };
            myModel1.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            _MainPlot.Model = myModel1;
            var myModel2 = new PlotModel { Title = "温度" };
            myModel2.Series.Add(new FunctionSeries(Math.Cos, 1, 12, 0.3, "cos(x)"));
            _TempPlot.Model = myModel2;
        }
    }
}