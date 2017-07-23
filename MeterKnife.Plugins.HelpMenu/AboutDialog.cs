using System;
using System.Windows.Forms;
using NKnife.Interface;
using NKnife.IoC;

namespace MeterKnife.Plugins.HelpMenu
{
    public sealed partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();

            SuspendLayout();
            var about = DI.Get<IAbout>();
            Text = $"关于 {about.AssemblyTitle}";
            labelProductName.Text = about.AssemblyProduct;
            labelVersion.Text = $"版本 {about.AssemblyVersion}";
            labelCopyright.Text = about.AssemblyCopyright;
            labelCompanyName.Text = about.AssemblyCompany;
            textBoxDescription.Text = about.AssemblyDescription;
            ResumeLayout(false);
        }
    }
}