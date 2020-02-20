using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Properties;

namespace NKnife.MeterKnife.Workbench.Menus
{
    public sealed class MeasureMenuItem : ToolStripMenuItem
    {
        public MeasureMenuItem()
        {
            Text = this.Res("测量(&M)");
            var start = new ToolStripMenuItem(this.Res("启动"));
            start.Image = Resources.start_24px;
            start.ShortcutKeys = Keys.Control | Keys.F5;
            DropDownItems.Add(start);

            var pause = new ToolStripMenuItem(this.Res("暂停"));
            pause.ShortcutKeys = Keys.Control | Keys.F6;
            pause.Image = Resources.pause_24px;
            DropDownItems.Add(pause);

            var stop = new ToolStripMenuItem(this.Res("停止"));
            stop.ShortcutKeys = Keys.Control | Keys.F4;
            stop.Image = Resources.stop_24px;
            DropDownItems.Add(stop);
        }
    }
}
