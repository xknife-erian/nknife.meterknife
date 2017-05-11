using System;
using WixSharp;

namespace MeterKnife.Setup
{
    public static class Script
    {
        public static void Main(string[] args)
        {
            var project = new Project("meterknife",
                new Dir(@"%ProgramFiles%\xknife\meterknife",
                 new File(@"*.dll")));
            project.GUID = new Guid("6f330b47-2577-43ad-9095-1861ba25889b");
            Compiler.BuildMsi(project);
        }
    }
}