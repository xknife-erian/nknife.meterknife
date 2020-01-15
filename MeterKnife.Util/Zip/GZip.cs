using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using NKnife.Utility;
using NKnife.Wrapper.Files;

namespace NKnife.Zip
{
    /// <summary>支持多种多个文件/文件夹的压缩和解压还原(支持中文).工作原理:读取多文件,格式化后,按照某种规则保存到一个文件中
    /// </summary>
    public class GZip
    {
        /// <summary>
        /// Compress
        /// </summary>
        /// <param name="lpSourceFolder">The location of the files to include in the zip file, all files including files in subfolders will be included.</param>
        /// <param name="lpDestFolder">Folder to write the zip file into</param>
        /// <param name="zipFileName">Name of the zip file to write</param>
        public static GZipResult Compress(string lpSourceFolder, string lpDestFolder, string zipFileName)
        {
            return Compress(lpSourceFolder, "*.*", SearchOption.AllDirectories, lpDestFolder, zipFileName, true);
        }

        /// <summary>
        /// Compress
        /// </summary>
        /// <param name="lpSourceFolder">The location of the files to include in the zip file</param>
        /// <param name="searchPattern">Search pattern (ie "*.*" or "*.txt" or "*.gif") to idendify what files in lpSourceFolder to include in the zip file</param>
        /// <param name="searchOption">Only files in lpSourceFolder or include files in subfolders also</param>
        /// <param name="lpDestFolder">Folder to write the zip file into</param>
        /// <param name="zipFileName">Name of the zip file to write</param>
        /// <param name="deleteTempFile">Boolean, true deleted the intermediate temp file, false leaves the temp file in lpDestFolder (for debugging)</param>
        public static GZipResult Compress(string lpSourceFolder, string searchPattern, SearchOption searchOption, string lpDestFolder, string zipFileName, bool deleteTempFile)
        {
            var di = new DirectoryInfo(lpSourceFolder);
            FileInfo[] files = di.GetFiles("*.*", searchOption);
            return Compress(files, lpSourceFolder, lpDestFolder, zipFileName, deleteTempFile);
        }

        /// <summary>
        /// Compress
        /// </summary>
        /// <param name="files">Array of FileInfo objects to be included in the zip file</param>
        /// <param name="folders">Array of Folder string</param>
        /// <param name="lpBaseFolder">Base folder to use when creating relative paths for the files 
        /// stored in the zip file. For example, if lpBaseFolder is 'C:\zipTest\Files\', and there is a file 
        /// 'C:\zipTest\Files\folder1\sample.txt' in the 'files' array, the relative path for sample.txt 
        /// will be 'folder1/sample.txt'</param>
        /// <param name="lpDestFolder">Folder to write the zip file into</param>
        /// <param name="zipFileName">Name of the zip file to write</param>
        public static GZipResult Compress(FileInfo[] files, string[] folders, string lpBaseFolder, string lpDestFolder, string zipFileName)
        {
            //support compress folder
            IList<FileInfo> list = files.ToList();

            foreach (string str in folders)
            {
                var di = new DirectoryInfo(str);
                foreach (FileInfo info in di.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    list.Add(info);
                }
            }

            return Compress(list.ToArray(), lpBaseFolder, lpDestFolder, zipFileName, true);
        }

        /// <summary>
        /// Compress
        /// </summary>
        /// <param name="files">Array of FileInfo objects to be included in the zip file</param>
        /// <param name="lpBaseFolder">Base folder to use when creating relative paths for the files 
        /// stored in the zip file. For example, if lpBaseFolder is 'C:\zipTest\Files\', and there is a file 
        /// 'C:\zipTest\Files\folder1\sample.txt' in the 'files' array, the relative path for sample.txt 
        /// will be 'folder1/sample.txt'</param>
        /// <param name="lpDestFolder">Folder to write the zip file into</param>
        /// <param name="zipFileName">Name of the zip file to write</param>
        public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName)
        {
            return Compress(files, lpBaseFolder, lpDestFolder, zipFileName, true);
        }

        /// <summary>
        /// Compress
        /// </summary>
        /// <param name="files">Array of FileInfo objects to be included in the zip file</param>
        /// <param name="lpBaseFolder">Base folder to use when creating relative paths for the files 
        /// stored in the zip file. For example, if lpBaseFolder is 'C:\zipTest\Files\', and there is a file 
        /// 'C:\zipTest\Files\folder1\sample.txt' in the 'files' array, the relative path for sample.txt 
        /// will be 'folder1/sample.txt'</param>
        /// <param name="lpDestFolder">Folder to write the zip file into</param>
        /// <param name="zipFileName">Name of the zip file to write</param>
        /// <param name="deleteTempFile">Boolean, true deleted the intermediate temp file, false leaves the temp file in lpDestFolder (for debugging)</param>
        public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName, bool deleteTempFile)
        {
            var result = new GZipResult();

            try
            {
                if (!lpDestFolder.EndsWith("\\"))
                {
                    lpDestFolder += "\\";
                }

                string lpTempFile = lpDestFolder + zipFileName + ".tmp";
                string lpZipFile = lpDestFolder + zipFileName;

                result.TempFile = lpTempFile;
                result.ZipFile = lpZipFile;

                if (files != null && files.Length > 0)
                {
                    CreateTempFile(files, lpBaseFolder, lpTempFile, result);

                    if (result.FileCount > 0)
                    {
                        CreateZipFile(lpTempFile, lpZipFile, result);
                    }

                    // delete the temp file
                    if (deleteTempFile)
                    {
                        File.Delete(lpTempFile);
                        result.TempFileDeleted = true;
                    }
                }
            }
            catch //(Exception ex4)
            {
                result.Errors = true;
            }
            return result;
        }

        private static void CreateZipFile(string lpSourceFile, string lpZipFile, GZipResult result)
        {
            FileStream fsOut = null;
            FileStream fsIn = null;
            GZipStream gzip = null;

            // compress the file into the zip file
            try
            {
                fsOut = new FileStream(lpZipFile, FileMode.Create, FileAccess.Write, FileShare.None);
                gzip = new GZipStream(fsOut, CompressionMode.Compress, true);

                fsIn = new FileStream(lpSourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                var buffer = new byte[fsIn.Length];
                fsIn.Read(buffer, 0, buffer.Length);
                fsIn.Close();
                fsIn = null;

                // compress to the zip file
                gzip.Write(buffer, 0, buffer.Length);

                result.ZipFileSize = fsOut.Length;
                result.CompressionPercent = GetCompressionPercent(result.TempFileSize, result.ZipFileSize);
            }
            catch //(Exception ex1)
            {
                result.Errors = true;
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                }
                if (fsOut != null)
                {
                    fsOut.Close();
                }
                if (fsIn != null)
                {
                    fsIn.Close();
                }
            }
        }

        private static void CreateTempFile(FileInfo[] files, string lpBaseFolder, string lpTempFile, GZipResult result)
        {
            int fileIndex = 0;
            FileStream fsOut = null;
            FileStream fsIn = null;

            if (files != null && files.Length > 0)
            {
                try
                {
                    result.Files = new GZipFileInfo[files.Length];

                    // open the temp file for writing
                    fsOut = new FileStream(lpTempFile, FileMode.Create, FileAccess.Write, FileShare.None);

                    foreach (FileInfo fi in files)
                    {
                        try
                        {
                            var gzf = new GZipFileInfo {Index = fileIndex};

                            // read the source file, get its virtual path within the source folder
                            string lpSourceFile = fi.FullName;
                            gzf.LocalPath = lpSourceFile;
                            string vpSourceFile = lpSourceFile.Replace(lpBaseFolder, string.Empty);
                            vpSourceFile = vpSourceFile.Replace("\\", "/");
                            gzf.RelativePath = vpSourceFile;

                            fsIn = new FileStream(lpSourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                            var buffer = new byte[fsIn.Length];
                            fsIn.Read(buffer, 0, buffer.Length);
                            fsIn.Close();
                            fsIn = null;

                            string fileModDate = fi.LastWriteTimeUtc.ToString();
                            gzf.ModifiedDate = fi.LastWriteTimeUtc;
                            gzf.Length = buffer.Length;

                            string fileHeader = fileIndex.ToString() + "," + vpSourceFile + "," + fileModDate + "," + buffer.Length.ToString() + "\n";
                            byte[] header = Encoding.Default.GetBytes(fileHeader);

                            fsOut.Write(header, 0, header.Length);
                            fsOut.Write(buffer, 0, buffer.Length);
                            fsOut.WriteByte(10); // linefeed

                            gzf.AddedToTempFile = true;

                            // update the result object
                            result.Files[fileIndex] = gzf;

                            // increment the fileIndex
                            fileIndex++;
                        }
                        catch //(Exception ex1)
                        {
                            result.Errors = true;
                        }
                        finally
                        {
                            if (fsIn != null)
                            {
                                fsIn.Close();
                                fsIn = null;
                            }
                        }
                        result.TempFileSize = fsOut.Length;
                    }
                }
                catch //(Exception ex2)
                {
                    result.Errors = true;
                }
                finally
                {
                    if (fsOut != null)
                    {
                        fsOut.Close();
                    }
                }
            }

            result.FileCount = fileIndex;
        }

        public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName)
        {
            return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, true, null, null, 4096);
        }

        public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName, bool writeFiles, string addExtension)
        {
            return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, writeFiles, addExtension, null, 4096);
        }

        public static GZipResult Decompress(string lpSrcFolder, string lpDestFolder, string zipFileName, bool deleteTempFile, bool writeFiles, string addExtension, Hashtable htFiles, int bufferSize)
        {
            var result = new GZipResult();

            if (!lpSrcFolder.EndsWith("\\"))
            {
                lpSrcFolder += "\\";
            }

            if (!lpDestFolder.EndsWith("\\"))
            {
                lpDestFolder += "\\";
            }

            string lpTempFile = lpSrcFolder + zipFileName + ".tmp";
            string lpZipFile = lpSrcFolder + zipFileName;

            result.TempFile = lpTempFile;
            result.ZipFile = lpZipFile;

            FileStream fsTemp = null;
            var gzfs = new ArrayList();

            if (string.IsNullOrEmpty(addExtension))
            {
                addExtension = string.Empty;
            }
            else if (!addExtension.StartsWith("."))
            {
                addExtension = "." + addExtension;
            }

            // extract the files from the temp file
            try
            {
                fsTemp = UnzipToTempFile(lpZipFile, lpTempFile, result);
                if (fsTemp != null)
                {
                    while (fsTemp.Position != fsTemp.Length)
                    {
                        string line = null;
                        while (string.IsNullOrEmpty(line) && fsTemp.Position != fsTemp.Length)
                        {
                            line = ReadLine(fsTemp);
                        }

                        if (!string.IsNullOrEmpty(line))
                        {
                            var gzf = new GZipFileInfo();
                            if (gzf.ParseFileInfo(line) && gzf.Length > 0)
                            {
                                gzfs.Add(gzf);
                                string lpFilePath = lpDestFolder + gzf.RelativePath;
                                string lpFolder = GetFolder(lpFilePath);
                                gzf.LocalPath = lpFilePath;

                                bool write = false;
                                if (htFiles == null || htFiles.ContainsKey(gzf.RelativePath))
                                {
                                    gzf.RestoreRequested = true;
                                    write = writeFiles;
                                }

                                if (write)
                                {
                                    // make sure the folder exists
                                    if (!Directory.Exists(lpFolder))
                                    {
                                        UtilityFile.CreateDirectory(lpFolder);
                                    }

                                    // read from fsTemp and write out the file
                                    gzf.Restored = WriteFile(fsTemp, gzf.Length, lpFilePath + addExtension, bufferSize);
                                }
                                else
                                {
                                    // need to advance fsTemp
                                    fsTemp.Position += gzf.Length;
                                }
                            }
                        }
                    }
                }
            }
            catch //(Exception ex3)
            {
                result.Errors = true;
            }
            finally
            {
                if (fsTemp != null)
                {
                    fsTemp.Close();
                }
            }

            // delete the temp file
            try
            {
                if (deleteTempFile)
                {
                    File.Delete(lpTempFile);
                    result.TempFileDeleted = true;
                }
            }
            catch //(Exception ex4)
            {
                result.Errors = true;
            }

            result.FileCount = gzfs.Count;
            result.Files = new GZipFileInfo[gzfs.Count];
            gzfs.CopyTo(result.Files);
            return result;
        }

        private static string ReadLine(FileStream fs)
        {
            const int bufferSize = 4096;
            var buffer = new byte[bufferSize];
            byte b = 0;
            const byte lf = 10;
            int i = 0;

            while (b != lf)
            {
                b = (byte) fs.ReadByte();
                buffer[i] = b;
                i++;
            }

            string line = Encoding.Default.GetString(buffer, 0, i - 1);

            return line;
        }

        private static bool WriteFile(FileStream fs, int fileLength, string lpFile, int bufferSize)
        {
            bool success;
            FileStream fsFile = null;

            if (bufferSize == 0 || fileLength < bufferSize)
            {
                bufferSize = fileLength;
            }

            int remaining = fileLength;

            try
            {
                var buffer = new byte[bufferSize];
                fsFile = new FileStream(lpFile, FileMode.Create, FileAccess.Write, FileShare.None);

                while (remaining > 0)
                {
                    int readSize = remaining > bufferSize ? bufferSize : remaining;

                    int count = fs.Read(buffer, 0, readSize);
                    remaining -= count;

                    if (count == 0)
                    {
                        break;
                    }

                    fsFile.Write(buffer, 0, count);
                    fsFile.Flush();
                }
                fsFile.Flush();
                fsFile.Close();
                fsFile = null;

                success = true;
            }
            catch //(Exception ex2)
            {
                success = false;
            }
            finally
            {
                if (fsFile != null)
                {
                    fsFile.Flush();
                    fsFile.Close();
                }
            }
            return success;
        }

        private static string GetFolder(string lpFilePath)
        {
            string lpFolder = lpFilePath;
            int index = lpFolder.LastIndexOf("\\");
            if (index != -1)
            {
                lpFolder = lpFolder.Substring(0, index + 1);
            }
            return lpFolder;
        }

        private static FileStream UnzipToTempFile(string lpZipFile, string lpTempFile, GZipResult result)
        {
            FileStream fsIn = null;
            GZipStream gzip = null;
            FileStream fsOut = null;

            const int bufferSize = 4096;
            var buffer = new byte[bufferSize];

            try
            {
                fsIn = new FileStream(lpZipFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                result.ZipFileSize = fsIn.Length;

                fsOut = new FileStream(lpTempFile, FileMode.Create, FileAccess.Write, FileShare.None);
                gzip = new GZipStream(fsIn, CompressionMode.Decompress, true);
                while (true)
                {
                    int count = gzip.Read(buffer, 0, bufferSize);
                    if (count != 0)
                    {
                        fsOut.Write(buffer, 0, count);
                    }
                    if (count != bufferSize)
                    {
                        break;
                    }
                }
            }
            catch //(Exception ex1)
            {
                result.Errors = true;
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                }
                if (fsOut != null)
                {
                    fsOut.Close();
                }
                if (fsIn != null)
                {
                    fsIn.Close();
                }
            }

            var fsTemp = new FileStream(lpTempFile, FileMode.Open, FileAccess.Read, FileShare.None);
            result.TempFileSize = fsTemp.Length;
            return fsTemp;
        }

        private static int GetCompressionPercent(long tempLen, long zipLen)
        {
            double tmp = tempLen;
            double zip = zipLen;
            const double hundred = 100;

            double ratio = (tmp - zip)/tmp;
            double pcnt = ratio*hundred;

            return (int) pcnt;
        }
    }
}