using System;
using System.Windows.Forms;
using NKnife.Interface;
using NKnife.IoC;

namespace MeterKnife.Workbench.Dialogs
{
    sealed partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            var about = DI.Get<IAbout>();
            Text = String.Format("关于 {0}", about.AssemblyTitle);
            labelProductName.Text = about.AssemblyProduct;
            labelVersion.Text = String.Format("版本 {0}", about.AssemblyVersion);
            labelCopyright.Text = about.AssemblyCopyright;
            labelCompanyName.Text = about.AssemblyCompany;
            textBoxDescription.Text = about.AssemblyDescription;
        }
    }
}