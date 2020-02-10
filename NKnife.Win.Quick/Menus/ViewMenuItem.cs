using System.Collections.Generic;
using System.Windows.Forms;
using NKnife.Win.Quick.Base;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick.Menus
{
    public sealed class ViewMenuItem : ToolStripMenuItem
    {
        private readonly Dictionary<string, ToolStripMenuItem> _themeMenuItems = new Dictionary<string, ToolStripMenuItem>();
        private readonly Dictionary<string, ToolStripMenuItem> _cultureMenuItems = new Dictionary<string, ToolStripMenuItem>();

        public ViewMenuItem()
        {
            Text = this.Res("视图(&V)");
            SetThemeMenu();
            SetCultureMenu();
        }

        private void SetCultureMenu()
        {
            var lang = new ToolStripMenuItem(this.Res("语言(&L)"));
            var zh = new ToolStripMenuItem("简体中文");
            zh.Click += (s, e) => { SetCultureAction(s, "zh-CN"); };
            _cultureMenuItems.Add("zh-CN", zh);
            var tw = new ToolStripMenuItem("繁体中文");
            tw.Click += (s, e) => { SetCultureAction(s, "zh-TW"); };
            _cultureMenuItems.Add("zh-TW", tw);
            var en = new ToolStripMenuItem("英文");
            en.Click += (s, e) => { SetCultureAction(s, "en"); };
            _cultureMenuItems.Add("en", en);
            lang.DropDownItems.AddRange(new ToolStripItem[] {zh, tw, en});
            DropDownItems.Add(lang);
        }

        private void SetCultureAction(object sender, string cultureName)
        {
            Global.Culture = cultureName;
            var form = Parent.FindForm();
            if (form != null && form is IWorkbench wb)
            {
                wb.SetHabitAction.Invoke(nameof(Global.Culture), cultureName);
                SetActiveCulture(cultureName);
            }
        }

        public void SetActiveCulture(string cultureName)
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
            t11.Click += (s, e) => { SetThemeAction(s, new VS2015BlueTheme()); };

            var t12 = new ToolStripMenuItem("现代深色");
            _themeMenuItems.Add(nameof(VS2015DarkTheme), t12);
            t12.Click += (s, e) => { SetThemeAction(s, new VS2015DarkTheme()); };

            var t13 = new ToolStripMenuItem("现代亮色");
            _themeMenuItems.Add(nameof(VS2015LightTheme), t13);
            t13.Click += (s, e) => { SetThemeAction(s, new VS2015LightTheme()); };

            var t7 = new ToolStripMenuItem("经典蓝色");
            _themeMenuItems.Add(nameof(VS2013BlueTheme), t7);
            t7.Click += (s, e) => { SetThemeAction(s, new VS2013BlueTheme()); };

            var t9 = new ToolStripMenuItem("经典深色");
            _themeMenuItems.Add(nameof(VS2013DarkTheme), t9);
            t9.Click += (s, e) => { SetThemeAction(s, new VS2013DarkTheme()); };

            var t10 = new ToolStripMenuItem("经典亮色");
            _themeMenuItems.Add(nameof(VS2013LightTheme), t10);
            t10.Click += (s, e) => { SetThemeAction(s, new VS2013LightTheme()); };

            var t4 = new ToolStripMenuItem("灵动蓝色");
            _themeMenuItems.Add(nameof(VS2012BlueTheme), t4);
            t4.Click += (s, e) => { SetThemeAction(s, new VS2012BlueTheme()); };

            var t5 = new ToolStripMenuItem("灵动深色");
            _themeMenuItems.Add(nameof(VS2012DarkTheme), t5);
            t5.Click += (s, e) => { SetThemeAction(s, new VS2012DarkTheme()); };

            var t6 = new ToolStripMenuItem("灵动亮色");
            _themeMenuItems.Add(nameof(VS2012LightTheme), t6);
            t6.Click += (s, e) => { SetThemeAction(s, new VS2012LightTheme()); };

            var t2 = new ToolStripMenuItem("优雅速度");
            _themeMenuItems.Add(nameof(VS2005MultithreadingTheme), t2);
            t2.Click += (s, e) => { SetThemeAction(s, new VS2005MultithreadingTheme()); };

            var t3 = new ToolStripMenuItem("优雅");
            _themeMenuItems.Add(nameof(VS2005Theme), t3);
            t3.Click += (s, e) => { SetThemeAction(s, new VS2005Theme()); };

            var t1 = new ToolStripMenuItem(this.Res("传统"));
            _themeMenuItems.Add(nameof(VS2003Theme), t1);
            t1.Click += (s, e) => { SetThemeAction(s, new VS2003Theme()); };

            theme.DropDownItems.AddRange(new ToolStripItem[]
                {t11, t12, t13, new ToolStripSeparator(), t7, t9, t10, new ToolStripSeparator(), t4, t5, t6, new ToolStripSeparator(), t2, t3, t1});
            DropDownItems.Add(theme);
        }

        private void SetThemeAction(object sender, ThemeBase theme)
        {
            var form = Parent.FindForm();
            if (form != null && form is IWorkbench wb)
            {
                wb.MainDockPanel.Theme = theme;
                var themeName = theme.GetType().Name;
                wb.SetHabitAction.Invoke("MainTheme", themeName);
                SetActiveTheme(themeName);
            }
        }

        public void SetActiveTheme(string themeName)
        {
            foreach (var menu in _themeMenuItems.Values)
                menu.CheckState = CheckState.Unchecked;
            _themeMenuItems[themeName].CheckState = CheckState.Checked;
        }
    }
}