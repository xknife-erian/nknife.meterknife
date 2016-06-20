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
using Common.Logging;
using MeterKnife.Scpis.Properties;
using NKnife;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.Scpis
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
            Global.Culture = Common.Properties.Settings.Default.CultureInfoName;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DI.AssmeblyNameFilters = new[] { "DirectX", "CommPort" };
            DI.Initialize();
            LogManager.GetLogger<ScpiMangerForm>();
            Application.Run(new ScpiMangerForm());
        }
    }
}
