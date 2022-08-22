using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NKnife.Win.Quick.Base;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick.Menus
{
    public sealed class HelpMenuItem : ToolStripMenuItem
    {
        private readonly Dictionary<string, ToolStripMenuItem> _cultureMenuItems =
            new Dictionary<string, ToolStripMenuItem>();

        private readonly Dictionary<string, ToolStripMenuItem> _themeMenuItems =
            new Dictionary<string, ToolStripMenuItem>();

        private readonly IWorkbench _workbench;

        public HelpMenuItem(IWorkbench workbench)
        {
            _workbench = workbench;
            Text = this.Res("帮助(&H)");

            var update = new ToolStripMenuItem(this.Res("更新(&U)"));
            update.Click += UpdateMenu_OnClick;
            DropDownItems.Add(update);
            DropDownItems.Add(new ToolStripSeparator());

            SetThemeMenu();
            SetCultureMenu();

            DropDownItems.Add(new ToolStripSeparator());

            var about = new ToolStripMenuItem(this.Res("关于(&A)"));
            DropDownItems.Add(about);

            var culture = workbench.GetHabitValueFunc?.Invoke(nameof(Global.Culture), Global.Culture);
            var themeName = workbench.GetHabitValueFunc?.Invoke("MainTheme", nameof(VS2015BlueTheme));
            if (culture != null)
                SetActiveCultureAtMenu(culture.ToString());
            if (themeName != null)
                SetActiveThemeAtMenu(themeName.ToString());
        }

        private void UpdateMenu_OnClick(object sender, EventArgs e)
        {
            // var form = Parent.FindForm();
            // if (form != null && form is IWorkbench wb)
            // {
            //     _workbench = wb;
            //     var swVersion = Assembly.GetEntryAssembly()?.GetName().Version;
            //     if (true)//Helper.TryGetLatestRelease(wb.GithubUpdateUser, wb.GithubUpdateProject, out var latestRelease, out var errorMessage))
            //     {
            //         //var latestVersion = latestRelease.Version.TrimStart('v', 'V', '.', '-', '_').Trim();
            //         var latestVersion = swVersion.ToString();
            //         if (Version.TryParse(latestVersion, out var version))
            //         {
            //             if (version > swVersion)
            //             {
            //             } //UpdateHelper.PreparingCloseApplicationForUpdate(wb.GithubUpdateUser, wb.GithubUpdateProject, swVersion, StopApp);
            //             else
            //                 MessageBox.Show(this.Res("已是最新版本，无需更新。"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //         }
            //         else
            //         {
            //             //版本号对不上，也更新一下吧
            //             //UpdateHelper.PreparingCloseApplicationForUpdate(wb.GithubUpdateUser, wb.GithubUpdateProject, swVersion, StopApp);
            //         }
            //     }
            // }
        }

        private void StopApp()
        {
            _workbench.HideOnClosing = false;
            ((Form) _workbench).Close();
        }

        private void SetCultureMenu()
        {
            var lang = new ToolStripMenuItem(this.Res("语言(&L)"));
            var zh = new ToolStripMenuItem("简体中文");
            zh.Click += (s, e) => { SetCultureAction("zh-CN"); };
            _cultureMenuItems.Add("zh-CN", zh);
            var tw = new ToolStripMenuItem("繁体中文");
            tw.Click += (s, e) => { SetCultureAction("zh-TW"); };
            _cultureMenuItems.Add("zh-TW", tw);
            var en = new ToolStripMenuItem("英文");
            en.Click += (s, e) => { SetCultureAction("en"); };
            _cultureMenuItems.Add("en", en);
            lang.DropDownItems.AddRange(new ToolStripItem[] {zh, tw, en});
            DropDownItems.Add(lang);
        }

        private void SetCultureAction(string cultureName)
        {
            Global.Culture = cultureName;
            var form = Parent.FindForm();
            if (form != null && form is IWorkbench wb)
            {
                wb.SetHabitAction?.Invoke(nameof(Global.Culture), cultureName);
                SetActiveCultureAtMenu(cultureName);
            }
        }

        public void SetActiveCultureAtMenu(string cultureName)
        {
            foreach (var menu in _cultureMenuItems.Values)
                menu.CheckState = CheckState.Unchecked;
            _cultureMenuItems[cultureName].CheckState = CheckState.Checked;
        }

        private void SetThemeMenu()
        {
            var theme = new ToolStripMenuItem(this.Res("外观(&T)"));

            var t11 = new ToolStripMenuItem("现代蓝色");
            _themeMenuItems.Add(nameof(VS2015BlueTheme), t11);
            t11.Click += (s, e) => { SetThemeAction(new VS2015BlueTheme()); };

            var t12 = new ToolStripMenuItem("现代深色");
            _themeMenuItems.Add(nameof(VS2015DarkTheme), t12);
            t12.Click += (s, e) => { SetThemeAction(new VS2015DarkTheme()); };

            var t13 = new ToolStripMenuItem("现代亮色");
            _themeMenuItems.Add(nameof(VS2015LightTheme), t13);
            t13.Click += (s, e) => { SetThemeAction(new VS2015LightTheme()); };

            var t7 = new ToolStripMenuItem("经典蓝色");
            _themeMenuItems.Add(nameof(VS2013BlueTheme), t7);
            t7.Click += (s, e) => { SetThemeAction(new VS2013BlueTheme()); };

            var t9 = new ToolStripMenuItem("经典深色");
            _themeMenuItems.Add(nameof(VS2013DarkTheme), t9);
            t9.Click += (s, e) => { SetThemeAction(new VS2013DarkTheme()); };

            var t10 = new ToolStripMenuItem("经典亮色");
            _themeMenuItems.Add(nameof(VS2013LightTheme), t10);
            t10.Click += (s, e) => { SetThemeAction(new VS2013LightTheme()); };

            var t4 = new ToolStripMenuItem("灵动蓝色");
            _themeMenuItems.Add(nameof(VS2012BlueTheme), t4);
            t4.Click += (s, e) => { SetThemeAction(new VS2012BlueTheme()); };

            var t5 = new ToolStripMenuItem("灵动深色");
            _themeMenuItems.Add(nameof(VS2012DarkTheme), t5);
            t5.Click += (s, e) => { SetThemeAction(new VS2012DarkTheme()); };

            var t6 = new ToolStripMenuItem("灵动亮色");
            _themeMenuItems.Add(nameof(VS2012LightTheme), t6);
            t6.Click += (s, e) => { SetThemeAction(new VS2012LightTheme()); };

            theme.DropDownItems.AddRange(new ToolStripItem[]
                {t11, t12, t13, new ToolStripSeparator(), t7, t9, t10, new ToolStripSeparator(), t4, t5, t6});
            DropDownItems.Add(theme);
        }

        private void SetThemeAction(ThemeBase theme)
        {
            var form = Parent.FindForm();
            if (form != null && form is IWorkbench wb)
            {
                var themeName = theme.GetType().Name;
                wb.SetHabitAction?.Invoke("MainTheme", themeName);
                MessageBox.Show(Language.ResSection("SetThemeDialogText", "主题选择已保存，需要重启本程序以应用主题。"),
                    Language.Res("设置主题"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        ///     设置激活的主题显示在菜单上
        /// </summary>
        /// <param name="themeName">主题名</param>
        public void SetActiveThemeAtMenu(string themeName)
        {
            foreach (var menu in _themeMenuItems.Values)
                menu.CheckState = CheckState.Unchecked;
            _themeMenuItems[themeName].CheckState = CheckState.Checked;
        }
    }
}