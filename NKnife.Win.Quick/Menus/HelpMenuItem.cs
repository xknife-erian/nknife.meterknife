using System;
using System.Reflection;
using System.Windows.Forms;
using NKnife.Win.Quick.Base;
using NKnife.Win.UpdaterFromGitHub;

namespace NKnife.Win.Quick.Menus
{
    public sealed class HelpMenuItem : ToolStripMenuItem
    {
        private IWorkbench _workbench;

        public HelpMenuItem()
        {
            Text = this.String("帮助(&H)");
            var update = new ToolStripMenuItem(this.String("更新(&U)"));
            var about = new ToolStripMenuItem(this.String("关于(&A)"));
            DropDownItems.Add(update);
            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(about);
            update.Click += (s, e) =>
            {
                var form = Parent.FindForm();
                if (form != null && form is IWorkbench wb)
                {
                    _workbench = wb;
                    var swVersion = Assembly.GetEntryAssembly()?.GetName().Version;
                    if (Helper.TryGetLatestRelease(wb.GithubUpdateUser, wb.GithubUpdateProject, out var latestRelease, out var errorMessage))
                    {
                        var latestVersion = latestRelease.Version.TrimStart('v', 'V', '.', '-', '_').Trim();
                        if (Version.TryParse(latestVersion, out var version))
                        {
                            if (version > swVersion)
                                UpdateHelper.PreparingCloseApplicationForUpdate(wb.GithubUpdateUser, wb.GithubUpdateProject, swVersion, StopApp);
                            else
                                MessageBox.Show(this.String("已是最新版本，无需更新。"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //版本号对不上，也更新一下吧
                            UpdateHelper.PreparingCloseApplicationForUpdate(wb.GithubUpdateUser, wb.GithubUpdateProject, swVersion, StopApp);
                        }
                    }
                }
            };
        }

        private void StopApp()
        {
            _workbench.HideOnClosing = false;
            ((Form) _workbench).Close();
        }
    }
}