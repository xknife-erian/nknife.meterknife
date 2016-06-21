using System;
using WixSharp;

namespace MeterKnife.Setup
{
    internal class Program
    {
        private static void Main()
        {
            var dir = new Dir(@"%ProgramFiles%\My Company\My Product", new File("Program.cs"));
            //Compiler.WixLocation = @"C:\wix40-binaries";
            var project = new Project("MyProduct", dir);

            project.GUID = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b");
            //project.SourceBaseDir = "<input dir path>";
            //project.OutDir = "<output dir path>";

            project.BuildMsi();
        }
    }
}