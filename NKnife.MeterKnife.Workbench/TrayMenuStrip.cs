using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench
{
    public sealed class TrayMenuStrip : ContextMenuStrip
    {
        public TrayMenuStrip(IWorkbench workbench)
        {
            SuspendLayout();
            var separator1 = new ToolStripSeparator();
            var separator2 = new ToolStripSeparator();
            var separator3 = new ToolStripSeparator();
            var exitMenuButton = new ToolStripMenuItem();
            var openAppMenuButton = new ToolStripMenuItem();
            var engineeringManagerMenuButton = new ToolStripMenuItem();
            var dutManagerMenuButton = new ToolStripMenuItem();
            var currentMenuButton = new ToolStripMenuItem();
            Items.AddRange(new ToolStripItem[]
            {
                openAppMenuButton,
                separator1,
                currentMenuButton,
                separator2,
                engineeringManagerMenuButton,
                dutManagerMenuButton,
                separator3,
                exitMenuButton
            });
            exitMenuButton.Text = "退出(&X)";
            exitMenuButton.Click += (s, e) =>
            {
                Application.Exit();
            };
            openAppMenuButton.Text = "主程序(&W)";
            openAppMenuButton.Click += (s, e) =>
            {
                var form = (Form)workbench;
                form.ShowInTaskbar = true;  //显示在系统任务栏
                form.WindowState = FormWindowState.Normal;  //还原窗体
                form.Show();
                form.Activate();
            };
            engineeringManagerMenuButton.Text = "测试工程管理(&E)";
            dutManagerMenuButton.Text = "被测物管理(&D)";
            currentMenuButton.Text = "当前测试进程(&C)";
            ResumeLayout(false);
        }
    }
}