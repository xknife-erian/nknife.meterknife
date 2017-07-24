using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WixSharp;
using File = WixSharp.File;

namespace MeterKnife.Setup
{
    public static class Script
    {
        /**
         * WixSharp下载：https://github.com/oleg-shilo/wixsharp/releases
         * first you need prepare your building environment:
         * Download (and extract) Wix# binaries from the Releases section. 
         * You may but don’t have to download WiX as Wix# package already contains compatible copy of WiX.
         * 
         */

        /// <summary>
        /// 入口函数
        /// </summary>
        public static void Main(string[] args)
        {
            string model = "release";
#if DEBUG
            model = "Debug";
#else
            model = "Release";
#endif
            var file = new FileInfo($@"..\..\..\MeterKnife.App.Loader\bin\{model}\MeterKnife.exe");
            var project =
                new Project("MeterKnife",
                    //new Dir(@"%ProgramFiles%\XKNIFE\MeterKnife", new File(@"..\..\MeterKnife.App.Loader\bin\Debug\*.dll")),
                    new Dir(@"%ProgramFiles%\XKNIFE\MeterKnife", new File(file.FullName)));

            project.UI = WUI.WixUI_InstallDir;
            project.GUID = new Guid("6f330b47-2577-43ad-9095-1861ba25889b");
            project.EmitConsistentPackageId = true;
            project.PreserveTempFiles = true;

            project.BuildMsi();

            Console.WriteLine("===End==========");
            Console.WriteLine("Press any key end.");
            Console.ReadKey();
        }
    }
}