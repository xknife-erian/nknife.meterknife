using System.Windows.Forms;
using NKnife.Interface;
using NKnife.IoC;

namespace MeterKnife.Utils.Plots
{
    public sealed class PlotThemeDropDownButton : ToolStripDropDownButton
    {
        private readonly ToolStripMenuItem _DefaultThemeMenuItem = new ToolStripMenuItem("默认图表主题");
        private readonly ToolStripMenuItem _NewThemeMenuItem = new ToolStripMenuItem("新建图表主题");

        //private IUserApplicationData _HabitedDatas = DI.Get<IUserApplicationData>();

        public PlotThemeDropDownButton()
        {
            Text = "图表主题";
            DropDownItems.Add(_DefaultThemeMenuItem);
            DropDownItems.Add(_NewThemeMenuItem);
        }
    }
}