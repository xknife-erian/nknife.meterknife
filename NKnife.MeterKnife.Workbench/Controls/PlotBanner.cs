using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.Axes;

namespace NKnife.MeterKnife.Workbench.Controls
{
    public partial class PlotBanner : UserControl
    {
        private readonly List<Axis> _bindingAxes = new List<Axis>();

        public PlotBanner()
        {
            InitializeComponent();
        }

        public void AddAxes(List<Axis> axes)
        {
            for (int i = 0; i < axes.Count; i++)
            {
                var panel = new Panel();
                panel.BackColor = _TestColors[i];
                panel.Width = (int)axes[i].DesiredSize.Width;
                switch (axes[i].StartPosition)
                {
                    case 0:
                        panel.Dock = DockStyle.Left;
                        break;
                    case 1:
                        panel.Dock = DockStyle.Right;
                        break;
                }

                Controls.Add(panel);
            }
        }

        private static Color[] _TestColors = new Color[] {Color.MediumAquamarine, Color.DodgerBlue, Color.Chocolate, Color.DarkGray, Color.Firebrick, Color.Goldenrod};
    }
}
