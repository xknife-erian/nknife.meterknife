using System.Windows.Forms;
using NKnife.IoC;

namespace MeterKnife.Views.Menus
{
    public sealed class FileMenuItem : ToolStripMenuItem
    {
        private readonly ToolStripSeparator _ExitFrontSeparator = new ToolStripSeparator();
        private readonly ToolStripMenuItem _ExitMenuItem = new ToolStripMenuItem("退出(&X)");

        public FileMenuItem()
        {
            Text = "文件(&F)";
            DropDownItems.Add(_ExitFrontSeparator);
            DropDownItems.Add(_ExitMenuItem);
            _ExitMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.X;
            _ExitMenuItem.Click += (s, e) =>
            {
                DI.Get<Workbench>().Close();
            };
        }
    }
}
