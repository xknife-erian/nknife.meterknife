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
            var dir0 = BuildDir("MeterKnife.App.Loader", true, "MeterKnife");
            var dir1 = BuildDir("MeterKnife.App.Tray");
            var dir2 = BuildDir("MeterKnife.Commons");
            var dir3 = BuildDir("MeterKnife.Datas");
            var dir4 = BuildDir("MeterKnife.Gateway");
            var dir5 = BuildDir("MeterKnife.Kernel");
            var dir6 = BuildDir("MeterKnife.Reports");
            var dir7 = BuildDir("MeterKnife.Scpis");
            var dir8 = BuildDir("MeterKnife.ViewModels");
            var dir9 = BuildDir("MeterKnife.Views");
            var dir10 = BuildDir("MeterKnife.Views.Measures");
            var dir11 = BuildDir("MeterKnife.Plugins.FileMenu");
            var dir12 = BuildDir("MeterKnife.Plugins.HelpMenu");
            var dir13 = BuildDir("MeterKnife.Plugins.MeasureMenu");
            var dir14 = BuildDir("MeterKnife.Plugins.ViewMenu");

            var project = new Project("MeterKnife",
                dir0, dir1, dir2, dir3, dir4, dir5, dir6, dir7, dir8, dir9, dir10, dir11, dir12, dir13, dir14)
            {
                UI = WUI.WixUI_Common,
                GUID = new Guid("9f310cd4-7a59-4cf8-8ffe-bcebfde327b5"),
                EmitConsistentPackageId = true,
                PreserveTempFiles = true
            };

            project.BuildMsi();

            Console.WriteLine("===End==========");
            Console.WriteLine("Press any key end.");
            Console.ReadKey();
        }

        private static Dir BuildDir(string assemblyName, bool isExe = false, string name = "")
        {
            string model = "release";
#if DEBUG
            model = "Debug";
#else
            model = "Release";
#endif
            var f = @"..\..\..\";
            var m = $@"\bin\{model}\";
            var t = ".dll";
            if (isExe)
                t = ".exe";
            if (string.IsNullOrEmpty(name))
                name = assemblyName;
            var path = $"{f}{assemblyName}{m}{name}{t}";
            var fileInfo = new FileInfo(path);
            var dir = new Dir(@"%ProgramFiles%\XKNIFE\MeterKnife", new File(fileInfo.FullName));
            return dir;
        }
    }
}