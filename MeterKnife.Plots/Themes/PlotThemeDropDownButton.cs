using System.Windows.Forms;

namespace MeterKnife.Plots.Themes
{
    public sealed class PlotThemeDropDownButton : ToolStripDropDownButton
    {
        private readonly ToolStripMenuItem _defaultThemeMenuItem = new ToolStripMenuItem("默认主题(&D)");
        private readonly ToolStripMenuItem _themeManagerMenuItem = new ToolStripMenuItem("主题管理(&M)");

        public PlotThemeDropDownButton()
        {
            Text = "主题";
            DropDownItems.AddRange(new ToolStripItem[]
            {
                _defaultThemeMenuItem,
                new ToolStripSeparator(), 
                _themeManagerMenuItem
            });
            _themeManagerMenuItem.Click+= (s, e) =>
            {
                var dialog = new ThemeManagerDialog();
                dialog.ShowDialog(Owner.FindForm());
            };
        }
    }
}
