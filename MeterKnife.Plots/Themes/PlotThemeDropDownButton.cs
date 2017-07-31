using System.Windows.Forms;

namespace MeterKnife.Plots.Themes
{
    public sealed class PlotThemeDropDownButton : ToolStripDropDownButton
    {
        private readonly ToolStripMenuItem _DefaultThemeMenuItem = new ToolStripMenuItem("默认主题(&D)");
        private readonly ToolStripMenuItem _ThemeManagerMenuItem = new ToolStripMenuItem("主题管理(&M)");

        public PlotThemeDropDownButton()
        {
            Text = "主题";
            DropDownItems.AddRange(new ToolStripItem[]
            {
                _DefaultThemeMenuItem,
                new ToolStripSeparator(), 
                _ThemeManagerMenuItem
            });
            _ThemeManagerMenuItem.Click+= (s, e) =>
            {
                var dialog = new ThemeManagerDialog();
                dialog.ShowDialog(Owner.FindForm());
            };
        }
    }
}
