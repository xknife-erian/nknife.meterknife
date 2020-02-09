using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Win.Quick;
using NKnife.Win.Quick.Controls;
using NKnife.Win.Quick.Menus;
using NKnife.Win.QuickDemo.Menus;
using NKnife.Win.QuickDemo.Properties;

namespace NKnife.Win.QuickDemo
{
    public class DemoForm : QuickForm
    {
        public DemoForm()
        {
            GithubUpdateUser = "xknife-erian";
            GithubUpdateProject = "nknife.serial-protocol-debugger";
            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.demo;
            BindNotifyIcon(notifyIcon);

            var file = new FileMenuItem();
            file.DropDownItems.Insert(0, new ToolStripSeparator());
            file.DropDownItems.Insert(0, GetItem());

            BindMainMenu(file, new DataMenuItem(), new MeasureMenuItem(), new ToolMenuItem(), new ViewMenuItem(), new HelpMenuItem());
            BindTrayMenu(GetItem(), GetItem());
        }

        private ToolStripMenuItem GetItem()
        {
            var t = new ToolStripMenuItem("Abc");
            t.Click += (s, e) => { MessageBox.Show("Test"); };
            return t;
        }
    }
}
