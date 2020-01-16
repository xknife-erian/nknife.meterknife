using System;
using System.Reflection;

namespace MeterKnife.Util.Interface
{
    public interface IAbout
    {
        Assembly TargetAssembly { get; set; }
        string AssemblyTitle { get; }
        Version AssemblyVersion { get; }
        string AssemblyDescription { get; }
        string AssemblyProduct { get; }
        string AssemblyCopyright { get; }
        string AssemblyCompany { get; }
    }
}