using System.Windows.Forms;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick.Menus
{
    public sealed class ToolMenuItem : ToolStripMenuItem
    {
        private readonly LoggerDockContent _loggerDock = new LoggerDockContent();
        private readonly OptionDialog _optionDialog = new OptionDialog();

        public ToolMenuItem()
        {
            Text = this.Res("工具(&T)");
            var loggerMenu = new ToolStripMenuItem(this.Res("日志(&L)"));
            loggerMenu.Click += (s, e) =>
            {
                var form = Parent.FindForm();
                if (form != null && form is IWorkbench wb)
                    _loggerDock.Show(wb.MainDockPanel, DockState.DockBottom);
            };
            DropDownItems.Add(loggerMenu);
            var optionMenu = new ToolStripMenuItem(this.Res("选项(&O)"));
            optionMenu.Click += (s, e) =>
            {
                var form = Parent.FindForm();
                if (form != null && form is IWorkbench workbench)
                {
                    _optionDialog.InitializeOptionPanelList(workbench);
                    _optionDialog.ShowDialog(form);
                }
            };
            DropDownItems.Add(optionMenu);
        }
    }
}
