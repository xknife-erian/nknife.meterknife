using System.Windows.Forms;

namespace NKnife.MeterKnife.Workbench.Menus
{
    public sealed class MeasureMenuItem : ToolStripMenuItem
    {
        public MeasureMenuItem()
        {
            Text = this.Res("测量(&M)");
            var start = new ToolStripMenuItem(this.Res("启动"));
            start.ShortcutKeys = Keys.Control | Keys.F5;
            DropDownItems.Add(start);
            var pause = new ToolStripMenuItem(this.Res("暂停"));
            pause.ShortcutKeys = Keys.Control | Keys.F6;
            DropDownItems.Add(pause);
            var stop = new ToolStripMenuItem(this.Res("停止"));
            stop.ShortcutKeys = Keys.Control | Keys.F4;
            DropDownItems.Add(stop);
        }
    }
}
