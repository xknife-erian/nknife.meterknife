using System.Windows.Forms;

namespace NKnife.MeterKnife.Workbench.Dialogs.Plots
{
    public sealed class PlotThemeDropDownButton : ToolStripDropDownButton
    {
        private ToolStripMenuItem _defaultThemeMenuItem;
        private ToolStripMenuItem _themeManagerMenuItem;

        public PlotThemeDropDownButton()
        {
            Text = this.Res("主题");
            _defaultThemeMenuItem = new ToolStripMenuItem(this.Res("默认主题(&D)"));
            _themeManagerMenuItem = new ToolStripMenuItem(this.Res("主题管理(&M)"));
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
