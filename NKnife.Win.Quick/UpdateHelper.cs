using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NKnife.Win.Quick
{
    class UpdateHelper
    {
        /// <summary>
        ///     准备关闭应用程序进行更新
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="project"></param>
        /// <param name="swVersion"></param>
        /// <param name="preparedAction"></param>
        public static void PreparingCloseApplicationForUpdate(string userName, string project, Version swVersion, Action preparedAction)
        {
            var ds = MessageBox.Show(Language.ResSection("Update","即将更新。准备关闭应用程序进行更新。\r\n更新程序在更新过程中因为要覆盖原有文件，所以需要管理员权限才能更新。"), 
                Language.Res("准备更新"), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (ds != DialogResult.Yes)
                return;
            var swName = Assembly.GetEntryAssembly()?.GetName().Name;
            var arguments = $"--u {userName} --p {project} --r {swName}.exe --v {swVersion}";
            new Thread(() => Process.Start("UpdaterFromGitHub.exe", arguments)).Start();
            Thread.Sleep(2000);
            preparedAction();
        }
    }
}
