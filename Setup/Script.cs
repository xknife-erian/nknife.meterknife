using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
            WixEntity[] files = GetFiles();

            var dir = new Dir(@"%ProgramFiles%\XKNIFE\MeterKnife", files);

            var project = new Project("MeterKnife", dir);
            project.GUID = new Guid("9f310cd4-7a59-4cf8-8ffe-bcebfde327b5");
            project.UI = WUI.WixUI_Mondo;
            project.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            project.PreserveTempFiles = true;
            project.Properties = new[]
            {
                new Property("PubDesktop", @"C:\Users\Public\Desktop\")
            };
            project.BuildMsi();

            Console.WriteLine();
            Console.WriteLine("===End============");
            Console.WriteLine("Press any key end.");
            Console.ReadKey();
        }

        private static WixEntity[] GetFiles()
        {
            List<WixEntity> entities = new List<WixEntity>();

            BuildFile(entities, "MeterKnife.App.Loader", true, "MeterKnife");
            var path = entities[0].Name;

            var fi = new FileInfo(path);
            if (fi.DirectoryName != null)
            {
                var dir = new DirectoryInfo(fi.DirectoryName);
                var files = dir.GetFiles();
                foreach (var file in files)
                {
                    if (file.Extension == ".dll" || file.Extension == ".exe")
                    {
                        var f = new File(file.FullName);
                        entities.Add(f);
                        Console.WriteLine($">- {file.FullName}");
                    }
                }
            }

            return entities.ToArray();
        }

        private static void BuildFile(List<WixEntity> entities, string assemblyName, bool isExe = false, string name = "")
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
            var file = new File(fileInfo.FullName);

            entities.Add(file);
        }
    }
}