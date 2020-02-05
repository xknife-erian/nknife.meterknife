using System;
using System.Windows.Forms;
using NKnife.Interface;

namespace NKnife.MeterKnife.Workbench.Dialogs
{
    public sealed partial class AboutDialog : Form
    {
        public AboutDialog(IAbout about)
        {
            var about1 = about;
            InitializeComponent();
            Text = $"关于 {about1.AssemblyTitle}";
            labelProductName.Text = about1.AssemblyProduct;
            labelVersion.Text = $"版本 {about1.AssemblyVersion}";
            labelCopyright.Text = about1.AssemblyCopyright;
            labelCompanyName.Text = about1.AssemblyCompany;
            textBoxDescription.Text = about1.AssemblyDescription;
        }
    }
}