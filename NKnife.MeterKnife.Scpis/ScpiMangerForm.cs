using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NKnife.MeterKnife.Scpis.Properties;
using NKnife.Win.Forms;

namespace NKnife.MeterKnife.Scpis
{
    public partial class ScpiMangerForm : SimpleForm
    {
        public ScpiMangerForm()
        {
            InitializeComponent();
            Icon = Resources.scpi_manager;
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new ScpiMangerForm());
        }
    }
}
