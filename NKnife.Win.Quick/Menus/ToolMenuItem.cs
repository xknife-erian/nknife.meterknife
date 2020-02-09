using System.Windows.Forms;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick.Menus
{
    public sealed class ToolMenuItem : ToolStripMenuItem
    {
        private readonly LoggerDockContent _loggerDock = new LoggerDockContent();

        public ToolMenuItem()
        {
            Text = this.Res("工具(&T)");
            var menu = new ToolStripMenuItem(this.Res("日志(&L)"));
            menu.Click += (s, e) =>
            {
                var form = Parent.FindForm();
                if (form != null && form is IWorkbench wb)
                    _loggerDock.Show(wb.MainDockPanel, DockState.DockBottom);
            };
            DropDownItems.Add(menu);
        }
    }
}
