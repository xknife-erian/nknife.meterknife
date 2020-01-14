using System;
using System.IO;
using System.Reflection;
using NKnife.Interface;

namespace NKnife.Wrapper
{
    public class About : IAbout
    {
        public Assembly TargetAssembly { get; set; }

        public About()
        {
            TargetAssembly = Assembly.GetEntryAssembly();
        }

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = TargetAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                TargetAssembly = Assembly.GetExecutingAssembly();
                return Path.GetFileNameWithoutExtension(TargetAssembly.CodeBase);
            }
        }

        public Version AssemblyVersion
        {
            get { return TargetAssembly.GetName().Version; }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = TargetAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = TargetAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = TargetAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = TargetAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }
    }
}