using System.Collections.Generic;
using System.Windows.Forms;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick.Menus
{
    public sealed class FileMenuItem : ToolStripMenuItem
    {
        private IWorkbench _workbench;
        private readonly LoggerDockContent _loggerDock = new LoggerDockContent();
        private readonly OptionDialog _optionDialog = new OptionDialog();

        public FileMenuItem(IWorkbench workbench)
        {
            _workbench = workbench;
            Text = this.Res("文件(&F)");

            var loggerMenu = new ToolStripMenuItem(this.Res("日志(&L)"));
            loggerMenu.Click += (s, e) =>
            {
                    _loggerDock.Show(_workbench.MainDockPanel, DockState.DockBottom);
            };
            DropDownItems.Add(loggerMenu);

            var optionMenu = new ToolStripMenuItem(this.Res("选项(&O)"));
            optionMenu.Click += (s, e) =>
            {
                    _optionDialog.InitializeOptionPanelList(workbench);
                    _optionDialog.ShowDialog((Form)workbench);
            };
            DropDownItems.Add(optionMenu);
            DropDownItems.Add(new ToolStripSeparator());

            var exit = new ToolStripMenuItem(this.Res("退出(&X)"));
            exit.Click += (e, s) =>
            {
                ((Form)workbench).Close();
            };
            DropDownItems.Add(exit);
        }

    }
}
