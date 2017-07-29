using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Utils.Plots
{
    public sealed class PlotThemeDropDownButton : ToolStripDropDownButton
    {
        private readonly ToolStripMenuItem _DefaultThemeMenuItem = new ToolStripMenuItem("默认主题(&D)");
        private readonly ToolStripMenuItem _NewThemeMenuItem = new ToolStripMenuItem("新建主题(&N)");

        public PlotThemeDropDownButton()
        {
            Text = "主题";
            DropDownItems.AddRange(new ToolStripItem[]
            {
                _DefaultThemeMenuItem,
                _NewThemeMenuItem
            });
        }
    }
}
