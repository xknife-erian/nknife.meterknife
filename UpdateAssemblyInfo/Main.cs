using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace UpdateAssemblyInfo
{
    internal class MainClass
    {
        private const string BASE_COMMIT = "4ba2785f66bebeaa21c68cf4f6733fc19ddb0d9d";
        private const int BASE_COMMIT_REV = 0;

        private const string GLOBAL_ASSEMBLY_INFO_TEMPLATE_FILE = "src/Main/GlobalAssemblyInfo.cs.template";

        private static readonly TemplateFile[] _templateFiles =
        {
            new TemplateFile
            {
                Input = GLOBAL_ASSEMBLY_INFO_TEMPLATE_FILE,
                Output = "src/Main/GlobalAssemblyInfo.cs"
            },
            new TemplateFile
            {
                Input = "src/Main/GlobalAssemblyInfo.vb.template",
                Output = "src/Main/GlobalAssemblyInfo.vb"
            },
            new TemplateFile
            {
                Input = "src/Main/SharpDevelop/app.template.config",
                Output = "src/Main/SharpDevelop/app.config"
            },
            new TemplateFile
            {
                Input = "src/Setup/SharpDevelop.Setup.wixproj.user.template",
                Output = "src/Setup/SharpDevelop.Setup.wixproj.user"
            },
            new TemplateFile
            {
                Input = "src/AddIns/Misc/UsageDataCollector/UsageDataCollector.AddIn/AnalyticsMonitor.AppProperties.template",
                Output = "src/AddIns/Misc/UsageDataCollector/UsageDataCollector.AddIn/AnalyticsMonitor.AppProperties.cs"
            }
        };

        public static void Main(string[] args)
        {
            try
            {
                string exeDir = Path.GetDirectoryName(typeof (MainClass).Assembly.Location);
                if (string.IsNullOrEmpty(exeDir))
                    return;
                bool createdNew;
                using (var mutex = new Mutex(true, "MeterKnifeUpdateAssemblyInfo" + exeDir.GetHashCode(), out createdNew))
                {
                    if (!createdNew)
                    {
                        try
                        {
                            mutex.WaitOne(10000);
                        }
                        catch (AbandonedMutexException)
                        {
                        }
                        return;
                    }
                    if (!File.Exists("MeterKnife.sln"))
                    {
                        string mainDir = Path.GetFullPath(Path.Combine(exeDir, "../../.."));
                        if (File.Exists(mainDir + "\\MeterKnife.sln"))
                        {
                            Directory.SetCurrentDirectory(mainDir);
                        }
                    }
                    if (!File.Exists("MeterKnife.sln"))
                    {
                        Console.WriteLine("Working directory must be MeterKnife!");
                        return;
                    }
                    RetrieveRevisionNumber();
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (args[i] == "--branchname" && i + 1 < args.Length && !string.IsNullOrEmpty(args[i + 1]))
                            gitBranchName = args[i + 1];
                    }
                    UpdateFiles();
                    if (args.Contains("--REVISION"))
                    {
                        var doc = new XDocument(new XElement(
                            "versionInfo",
                            new XElement("version", fullVersionNumber),
                            new XElement("revision", revisionNumber),
                            new XElement("commitHash", gitCommitHash),
                            new XElement("branchName", gitBranchName),
                            new XElement("versionName", versionName)
                            ));
                        doc.Save("REVISION");
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void UpdateFiles()
        {
            foreach (TemplateFile file in _templateFiles)
            {
                string content;
                using (var r = new StreamReader(file.Input))
                {
                    content = r.ReadToEnd();
                }
                content = content.Replace("$INSERTVERSION$", fullVersionNumber);
                content = content.Replace("$INSERTMAJORVERSION$", majorVersionNumber);
                content = content.Replace("$INSERTREVISION$", revisionNumber);
                content = content.Replace("$INSERTCOMMITHASH$", gitCommitHash);
                content = content.Replace("$INSERTSHORTCOMMITHASH$", gitCommitHash.Substring(0, 8));
                content = content.Replace("$INSERTDATE$", DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
                content = content.Replace("$INSERTYEAR$", DateTime.Now.Year.ToString());
                content = content.Replace("$INSERTBRANCHNAME$", gitBranchName);
                bool isDefaultBranch = string.IsNullOrEmpty(gitBranchName) || gitBranchName == "master" || char.IsDigit(gitBranchName, 0);
                content = content.Replace("$INSERTBRANCHPOSTFIX$", isDefaultBranch ? "" : ("-" + gitBranchName));

                content = content.Replace("$INSERTVERSIONNAME$", versionName ?? "");
                content = content.Replace("$INSERTVERSIONNAMEPOSTFIX$", string.IsNullOrEmpty(versionName) ? "" : "-" + versionName);

                if (File.Exists(file.Output))
                {
                    using (var r = new StreamReader(file.Output))
                    {
                        if (r.ReadToEnd() == content)
                        {
                            continue;
                        }
                    }
                }
                using (var w = new StreamWriter(file.Output, false, Encoding.UTF8))
                {
                    w.Write(content);
                }
            }
        }

        private static void GetMajorVersion()
        {
            majorVersionNumber = "?";
            fullVersionNumber = "?";
            versionName = null;
            using (var r = new StreamReader(GLOBAL_ASSEMBLY_INFO_TEMPLATE_FILE))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    string search = "string Major = \"";
                    int pos = line.IndexOf(search);
                    if (pos >= 0)
                    {
                        int e = line.IndexOf('"', pos + search.Length + 1);
                        majorVersionNumber = line.Substring(pos + search.Length, e - pos - search.Length);
                    }
                    search = "string Minor = \"";
                    pos = line.IndexOf(search);
                    if (pos >= 0)
                    {
                        int e = line.IndexOf('"', pos + search.Length + 1);
                        majorVersionNumber = majorVersionNumber + "." + line.Substring(pos + search.Length, e - pos - search.Length);
                    }
                    search = "string Build = \"";
                    pos = line.IndexOf(search);
                    if (pos >= 0)
                    {
                        int e = line.IndexOf('"', pos + search.Length + 1);
                        fullVersionNumber = majorVersionNumber + "." + line.Substring(pos + search.Length, e - pos - search.Length) + "." + revisionNumber;
                    }
                    search = "string VersionName = \"";
                    pos = line.IndexOf(search);
                    if (pos >= 0)
                    {
                        int e = line.IndexOf('"', pos + search.Length + 1);
                        versionName = line.Substring(pos + search.Length, e - pos - search.Length);
                    }
                }
            }
        }

        private static void SetVersionInfo(string fileName, Regex regex, string replacement)
        {
            string content;
            using (var inFile = new StreamReader(fileName))
            {
                content = inFile.ReadToEnd();
            }
            string newContent = regex.Replace(content, replacement);
            if (newContent == content)
                return;
            using (var outFile = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                outFile.Write(newContent);
            }
        }

        #region Retrieve Revision Number

        private static string revisionNumber;
        private static string majorVersionNumber;
        private static string fullVersionNumber;

        /// <summary>
        ///     Descriptive version name, e.g. 'Beta 3'
        /// </summary>
        private static string versionName;

        private static string gitCommitHash;
        private static string gitBranchName;

        private static void RetrieveRevisionNumber()
        {
            if (revisionNumber == null)
            {
                if (Directory.Exists(".git"))
                {
                    try
                    {
                        ReadRevisionNumberFromGit();
                        ReadBranchNameFromGit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("There's no git working copy in " + Path.GetFullPath("."));
                }
            }

            if (revisionNumber == null)
            {
                ReadRevisionFromFile();
            }
            GetMajorVersion();
        }

        private static void ReadRevisionNumberFromGit()
        {
            var info = new ProcessStartInfo("cmd", "/c git rev-list " + BASE_COMMIT + "..HEAD");
            string path = Environment.GetEnvironmentVariable("PATH");
            path += ";" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "git\\bin");
            info.EnvironmentVariables["PATH"] = path;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            using (Process p = Process.Start(info))
            {
                string line;
                int revNum = BASE_COMMIT_REV;
                while ((line = p.StandardOutput.ReadLine()) != null)
                {
                    if (gitCommitHash == null)
                    {
                        gitCommitHash = line;
                    }
                    revNum++;
                }
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception("git-rev-list exit code was " + p.ExitCode);
                revisionNumber = revNum.ToString();
            }
        }

        private static void ReadBranchNameFromGit()
        {
            var info = new ProcessStartInfo("cmd", "/c git branch --no-color");
            string path = Environment.GetEnvironmentVariable("PATH");
            path += ";" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "git\\bin");
            info.EnvironmentVariables["PATH"] = path;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            using (Process p = Process.Start(info))
            {
                string line;
                gitBranchName = "(no branch)";
                while ((line = p.StandardOutput.ReadLine()) != null)
                {
                    if (line.StartsWith("* ", StringComparison.Ordinal))
                    {
                        gitBranchName = line.Substring(2);
                    }
                }
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new Exception("git-branch exit code was " + p.ExitCode);
            }
        }

        private static void ReadRevisionFromFile()
        {
            try
            {
                XDocument doc = XDocument.Load("REVISION");
                revisionNumber = (string) doc.Root.Element("revision");
                gitCommitHash = (string) doc.Root.Element("commitHash");
                gitBranchName = (string) doc.Root.Element("branchName");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("The revision number of the SharpDevelop version being compiled could not be retrieved.");
                Console.WriteLine();
                Console.WriteLine("Build continues with revision number '0'...");

                revisionNumber = null;
            }
            if (string.IsNullOrEmpty(revisionNumber))
            {
                revisionNumber = "0";
                gitCommitHash = "0000000000000000000000000000000000000000";
                //throw new ApplicationException("Error reading revision number");
            }
        }

        #endregion

        private class TemplateFile
        {
            public string Input { get; set; }
            public string Output { get; set; }
        }
    }
}