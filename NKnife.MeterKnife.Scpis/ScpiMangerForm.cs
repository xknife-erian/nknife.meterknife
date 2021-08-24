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
using Autofac;
using NKnife.MeterKnife.Scpis.Properties;
using IContainer = Autofac.IContainer;

namespace NKnife.MeterKnife.Scpis
{
    public partial class ScpiMangerForm : Form
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
            var builder = new ContainerBuilder();
            builder.RegisterType<ScpiInfoGetter>().As<IScpiInfoGetter>().SingleInstance();
            builder.RegisterType<ScpiMangerForm>().SingleInstance();
            builder.RegisterAssemblyTypes(typeof(ScpiMangerForm).Assembly)
                .Where(t => !t.IsAbstract && t.Name.EndsWith("Dialog"))
                .AsImplementedInterfaces()
                .AsSelf();
            using (AutofacContainer = builder.Build())
            {
                Application.Run(AutofacContainer.Resolve<ScpiMangerForm>());
            }
        }

        public static IContainer AutofacContainer { get; set; }
    }
}
