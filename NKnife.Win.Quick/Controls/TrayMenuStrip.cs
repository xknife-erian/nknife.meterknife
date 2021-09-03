using System.Windows.Forms;
using NKnife.Win.Quick.Base;

namespace NKnife.Win.Quick.Controls
{
    public sealed class TrayMenuStrip : ContextMenuStrip
    {
        public TrayMenuStrip(IWorkbench workbench)
        {
            SuspendLayout();
            var openAppMenuButton = new ToolStripMenuItem();
            var separator1 = new ToolStripSeparator();
            var exitMenuButton = new ToolStripMenuItem();
            Items.AddRange(new ToolStripItem[]
            {
                openAppMenuButton,
                separator1,
                exitMenuButton
            });
            exitMenuButton.Text = this.Res("退出(&X)");
            exitMenuButton.Click += (s, e) =>
            {
                workbench.HideOnClosing = false;
                if(workbench is Form form)
                    form.Close();
            };
            openAppMenuButton.Text = this.Res("主程序(&W)");
            openAppMenuButton.Click += (s, e) =>
            {
                var form = (Form)workbench;
                form.ShowInTaskbar = true;  //显示在系统任务栏
                form.WindowState = FormWindowState.Normal;  //还原窗体
                form.Show();
                form.Activate();
            };
            ResumeLayout(false);
        }

    }
}