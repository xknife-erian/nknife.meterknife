using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Win.Quick.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick
{
    public  sealed partial class LoggerDockContent : SingletonDockContent
    {
        public LoggerDockContent()
        {
            InitializeComponent();
            Text = this.Res("程序日志");
        }
    }
}
