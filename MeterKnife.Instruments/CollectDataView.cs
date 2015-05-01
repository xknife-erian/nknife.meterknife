using System;
using System.Drawing;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Instruments
{
    public partial class CollectDataView : DockContent
    {
        protected PlotView _Plot = new PlotView();

        public CollectDataView()
        {
            InitializeComponent();

            _Plot.Dock = DockStyle.Fill;
            _Plot.PanCursor = Cursors.Hand;
            _Plot.BackColor = Color.White;
            _Plot.ZoomHorizontalCursor = Cursors.SizeWE;
            _Plot.ZoomRectangleCursor = Cursors.SizeNWSE;
            _Plot.ZoomVerticalCursor = Cursors.SizeNS;
            _PlotPage.Controls.Add(_Plot);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var myModel = new PlotModel { Title = "Example 1" };
            myModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            _Plot.Model = myModel;
        }
    }
}