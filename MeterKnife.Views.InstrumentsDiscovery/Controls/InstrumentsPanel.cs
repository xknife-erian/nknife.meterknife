using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    public partial class InstrumentsPanel : UserControl
    {
        public InstrumentsPanel()
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                var cell = new InstrumentsDetailCell();
                cell.Dock = DockStyle.Top;
                cell.BringToFront();
                _ToolStripContainer.ContentPanel.Controls.Add(cell);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            var pen = new Pen(Color.LightSlateGray);

            var leftTop = Location;
            var rightTop = new Point(Width-1, Location.Y);
            var leftBottom = new Point(Location.X, Height-1);
            var rightBottom = new Point(Width-1, Height-1);

            g.DrawLine(pen, leftTop, rightTop);
            g.DrawLine(pen, leftTop, leftBottom);
            g.DrawLine(pen, rightTop, rightBottom);
            g.DrawLine(pen, leftBottom, rightBottom);
        }
    }
}
