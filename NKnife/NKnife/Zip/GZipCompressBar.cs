using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using NKnife.Encrypt;

namespace NKnife.Zip
{
    public class GZipCompressBar : ProgressBar
    {
        private const string FILE_EXTENSION_NAME = ".gcf"; // gzip compress file
        private const int MAX_FILE_TOTAL_LENGTH = 1024*1024*1024; // 1G
        private const int READ_BUFFER_SIZE = 8*1024; // 8K

        private List<GZipFileEntry> _FileEntryList = new List<GZipFileEntry>();
        private string _FolderDecompressTo = string.Empty;
        private string _GZipFileName = string.Empty;
        private int _NowMaxBarValue;
        private List<GZipFileEntry> _PacketEntryList = new List<GZipFileEntry>();

        private byte[] _ReadBuffer = new byte[READ_BUFFER_SIZE];

        public GZipCompressBar()
        {
        }

        public GZipCompressBar(string gzipFileName)
        {
            Visible = false;
            GZipFileName = gzipFileName;
        }

        public GZipCompressBar(string gzipFileName, string decompressFolder)
        {
            Visible = false;
            GZipFileName = gzipFileName;
            FolderDecompressTo = decompressFolder;
        }

        [Description("Set/Get GZip filename with extension .gcf")]
        public string GZipFileName
        {
            get { return _GZipFileName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _GZipFileName = value;
                    _PacketEntryList.Clear();
                }
                else
                {
                    if (!IsValidFileName(value))
                    {
                        MessageBox.Show("GZip filename or it's path contains invalid char.");
                    }
                    else if (Path.GetExtension(value).ToUpper() != FILE_EXTENSION_NAME.ToUpper())
                    {
                        MessageBox.Show("GZip filename must has extension " + FILE_EXTENSION_NAME + ".");
                    }
                    else
                    {
                        _PacketEntryList.Clear();
                        _GZipFileName = value;
                    }
                }
            }
        }

        [Description("Set/Get folder to decompress files")]
        public string FolderDecompressTo
        {
            get { return _FolderDecompressTo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _FolderDecompressTo = value;
                }
                else
                {
                    if (!Directory.Exists(value))
                    {
                        MessageBox.Show("Decompress folder: " + value + " does not exists.");
                    }
                    else
                    {
                        if (value.EndsWith(@"\"))
                        {
                            _FolderDecompressTo = value;
                        }
                        else
                        {
                            _FolderDecompressTo = value + @"\";
                        }
                    }
                }
            }
        }

        [Description("Get the default gzip file extension name.")]
        public string DefaultFileExtentionName
        {
            get { return FILE_EXTENSION_NAME; }
        }

        /// <summary>
        ///     按MS的模板, 只需要重载 Dispose 方法
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    _ReadBuffer = null;


                    _FileEntryList.Clear();
                    _FileEntryList = null;
                    _PacketEntryList.Clear();
                    _PacketEntryList = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        ///     Add a file to compress list.
        /// </summary>
        public bool AppendFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                MessageBox.Show("GZipFileName is empty or does not exist.");
                return false;
            }


            var addFileInfo = new GZipFileEntry(fileName);
            long totalLength = addFileInfo.OriginalLength;


            foreach (GZipFileEntry fileEntry in _FileEntryList)
            {
                if (fileEntry.FileName.ToUpper() == addFileInfo.FileName.ToUpper())
                {
                    MessageBox.Show("File: " + fileEntry.FileName + " has exists.");
                    return false;
                }
                totalLength += fileEntry.OriginalLength;
            }


            if (totalLength > MAX_FILE_TOTAL_LENGTH)
            {
                MessageBox.Show("Total files length is over " + (MAX_FILE_TOTAL_LENGTH/(1024*1024)) + "M.");
                return false;
            }


            _FileEntryList.Add(addFileInfo);
            return true;
        }

        /// <summary>
        ///     Clear all add files for compress.
        /// </summary>
        public void ClearFiles()
        {
            _FileEntryList.Clear();
            _PacketEntryList.Clear();
            SetStartPosition();
        }

        /// <summary>
        ///     Compress files with gzip algorithm.
        /// </summary>
        public bool Compress()
        {
            return Compressing();
        }

        /// <summary>
        ///     Decompress files from gzip file.
        /// </summary>
        public bool Decompress()
        {
            return Decompressing();
        }

        /// <summary>
        ///     If the gzipfile contains an assigned filename."
        /// </summary>
        public bool ContainsFile(string fileName)
        {
            if (!GetPacketEntryList())
            {
                return false;
            }


            string realFileName = Path.GetFileName(fileName.Trim());
            foreach (GZipFileEntry fileEntry in _PacketEntryList)
            {
                if (fileEntry.FileName.ToUpper() == realFileName.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     string format: filename|fileLength|gzippedLength|lastModifiedDate|lastAccessDate|creationgDate|fullFileName
        /// </summary>
        public string GetFileEntryStringByFileName(string fileName)
        {
            if (!GetPacketEntryList())
            {
                return null;
            }


            string realFileName = Path.GetFileName(fileName.Trim());
            foreach (GZipFileEntry fileEntry in _PacketEntryList)
            {
                if (fileEntry.FileName.ToUpper() == fileName.ToUpper())
                {
                    return fileEntry.FormattedStr;
                }
            }
            return null;
        }

        /// <summary>
        ///     string format: filename|fileLength|gzippedLength|lastModifiedDate|lastAccessDate|creationgDate|fullFileName
        /// </summary>
        public List<string> GetFileEntryStringList()
        {
            if (!GetPacketEntryList())
            {
                return null;
            }


            var fileInfoList = new List<string>();


            foreach (GZipFileEntry fileEntry in _PacketEntryList)
            {
                fileInfoList.Add(fileEntry.FormattedStr);
            }
            return fileInfoList;
        }

        private bool Compressing()
        {
            bool opSuccess = false;


            if (_FileEntryList.Count == 0)
            {
                MessageBox.Show("There has no compress file.");
                return opSuccess;
            }


            if (string.IsNullOrEmpty(_GZipFileName))
            {
                MessageBox.Show("GZipFileName is empty or not set.");
                return opSuccess;
            }


            SetApplicationCursor(Cursors.WaitCursor);


            try
            {
                using (var outStream = new FileStream(_GZipFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    _PacketEntryList.Clear();
                    WriteHeaderEmptyInfo(outStream); // 写文件长度字节, 压缩结束后再填实际数据
                    SetProgressBarMaxValue(false);
                    ShowBeginStep();


                    foreach (GZipFileEntry fileEntry in _FileEntryList)
                    {
                        SetProgressBarNowMaxValue(fileEntry, false);


                        fileEntry.WriteEntryInfo(outStream);
                        ShowProgressStep();
                        CompressFile(fileEntry, outStream);
                        _PacketEntryList.Add(fileEntry);
                    }


                    WriteHeaderLengthInfo(outStream); // 再填文件头, 此时有各块的长度信息
                }
                opSuccess = true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                ShowFinalStep();
                SetApplicationCursor(Cursors.Default);
            }
            return opSuccess;
        }

        private bool Decompressing()
        {
            bool opSuccess = false;


            if (string.IsNullOrEmpty(_FolderDecompressTo))
            {
                MessageBox.Show("Decompress folder is empty.");
                return opSuccess;
            }


            if (string.IsNullOrEmpty(_GZipFileName))
            {
                MessageBox.Show("GZipFileName is empty or does not exist.");
                return opSuccess;
            }


            SetApplicationCursor(Cursors.WaitCursor);


            try
            {
                using (var srcStream = new FileStream(_GZipFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    _PacketEntryList.Clear();
                    ReadHeaderLengthInfo(srcStream); // 获得各块的长度信息, 缺少文件项中的文件名、日期等信息
                    SetProgressBarMaxValue(true);
                    ShowBeginStep();


                    foreach (GZipFileEntry fileEntry in _PacketEntryList)
                    {
                        SetProgressBarNowMaxValue(fileEntry, true);


                        fileEntry.ReadEntryInfo(srcStream); // 读当前项的日期、文件名信息
                        ShowProgressStep();
                        DecompressFile(srcStream, fileEntry);
                        fileEntry.ResetFileDateTime(_FolderDecompressTo);
                    }
                }
                opSuccess = true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                ShowFinalStep();
                SetApplicationCursor(Cursors.Default);
            }


            return opSuccess;
        }

        /// <summary>
        ///     Compress one file.
        /// </summary>
        private void CompressFile(GZipFileEntry fileEntry, Stream outStream)
        {
            long preStreamPosition = outStream.Position;


            using (var srcStream = new FileStream(fileEntry.FileFullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var zipStream = new GZipStream(outStream, CompressionMode.Compress, true))
            {
                ShowProgressStep();


                int readCount = READ_BUFFER_SIZE;
                while (readCount == READ_BUFFER_SIZE)
                {
                    readCount = srcStream.Read(_ReadBuffer, 0, READ_BUFFER_SIZE);
                    zipStream.Write(_ReadBuffer, 0, readCount);


                    ShowProgressStep();
                }
            }


            fileEntry.GZipFileLength = (int) (outStream.Position - preStreamPosition); // 写入的长度
        }

        /// <summary>
        ///     Deompress one file.
        /// </summary>
        private void DecompressFile(Stream srcStream, GZipFileEntry fileEntry)
        {
            using (var outStream = new FileStream(_FolderDecompressTo + fileEntry.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var memStream = new MemoryStream())
            using (var zipStream = new GZipStream(memStream, CompressionMode.Decompress, true))
            {
                int gzipFileLength = fileEntry.GZipFileLength;
                int readCount;
                while (gzipFileLength > 0)
                {
                    int maxCount = Math.Min(gzipFileLength, READ_BUFFER_SIZE);

                    readCount = srcStream.Read(_ReadBuffer, 0, maxCount);
                    memStream.Write(_ReadBuffer, 0, readCount);


                    gzipFileLength -= readCount;


                    ShowProgressStep();
                }


                memStream.Position = 0;
                readCount = READ_BUFFER_SIZE;
                while (readCount == READ_BUFFER_SIZE)
                {
                    readCount = zipStream.Read(_ReadBuffer, 0, READ_BUFFER_SIZE);
                    outStream.Write(_ReadBuffer, 0, readCount);


                    ShowProgressStep();
                }
            }
            ShowProgressStep();
        }

        /// <summary>
        ///     写空头字节, 用于占位置
        /// </summary>
        private void WriteHeaderEmptyInfo(Stream outStream)
        {
            int headerSize = 1 + _FileEntryList.Count*3; // 前4个字节是文件数, 每个文件3部分, 分别是: 原文件长、压缩后长、文件项长
            var headerBytes = new byte[4*headerSize];
            outStream.Write(headerBytes, 0, headerBytes.Length);
        }

        /// <summary>
        ///     写实际的文件数、文件长度、项长度字节
        /// </summary>
        private void WriteHeaderLengthInfo(Stream outStream)
        {
            byte[] fileCountBytes = BitConverter.GetBytes(_PacketEntryList.Count);
            SimpleCipher.EncryptBytes(fileCountBytes);


            outStream.Position = 0;
            outStream.Write(fileCountBytes, 0, fileCountBytes.Length);


            foreach (GZipFileEntry entry in _PacketEntryList)
            {
                entry.WriteLengthInfo(outStream);
            }
        }

        private void ReadHeaderLengthInfo(Stream srcStream)
        {
            var fileCountBytes = new byte[4];
            srcStream.Read(fileCountBytes, 0, fileCountBytes.Length);
            SimpleCipher.EncryptBytes(fileCountBytes);


            int fileCount = BitConverter.ToInt32(fileCountBytes, 0);


            for (int k = 1; k <= fileCount; k++)
            {
                var entry = new GZipFileEntry();
                entry.ReadLengthInfo(srcStream);
                _PacketEntryList.Add(entry);
            }
        }

        private bool GetPacketEntryList()
        {
            if (_PacketEntryList.Count > 0)
            {
                return true;
            }


            if (string.IsNullOrEmpty(_GZipFileName) || !File.Exists(_GZipFileName))
            {
                MessageBox.Show("GZipFileName is empty or does not exist.");
                return false;
            }


            bool opSuccess = false;
            SetApplicationCursor(Cursors.WaitCursor);


            try
            {
                using (var srcStream = new FileStream(_GZipFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    ReadHeaderLengthInfo(srcStream);
                    foreach (GZipFileEntry fileEntry in _PacketEntryList)
                    {
                        fileEntry.ReadEntryInfo(srcStream);
                        srcStream.Position += fileEntry.GZipFileLength;
                    }
                }
                opSuccess = true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                SetApplicationCursor(Cursors.Default);
            }
            return opSuccess;
        }

        private int GetFileMaxStepLength(GZipFileEntry fileEntry, bool decompress)
        {
            int maxLength = Math.Max(fileEntry.OriginalLength, fileEntry.GZipFileLength);
            int stepValue = 0;


            if (decompress)
            {
                stepValue++; // 取文件项
                stepValue += 2*(maxLength/READ_BUFFER_SIZE); // 产生压缩流
                stepValue++; // 关闭文件
            }
            else
            {
                stepValue++; // 打开源文件
                stepValue++; // 写文件信息项
                stepValue += maxLength/READ_BUFFER_SIZE; // 压缩
            }


            return stepValue;
        }

        private void SetProgressBarMaxValue(bool decompress)
        {
            SetStartPosition();
            _NowMaxBarValue = 0;


            int maxBarValue = 1; // 打开/建立文件
            if (decompress)
            {
                foreach (GZipFileEntry fileEntry in _PacketEntryList)
                {
                    maxBarValue += GetFileMaxStepLength(fileEntry, decompress); // 加每个文件的步长
                }
            }
            else
            {
                foreach (GZipFileEntry fileEntry in _FileEntryList)
                {
                    maxBarValue += GetFileMaxStepLength(fileEntry, decompress); // 加每个文件的步长
                }
            }
            maxBarValue += 1; // 最后收尾
            Maximum = maxBarValue;
        }

        private void SetProgressBarNowMaxValue(GZipFileEntry fileEntry, bool decompress)
        {
            _NowMaxBarValue += GetFileMaxStepLength(fileEntry, decompress);
        }

        /// <summary>
        ///     设置当前控件及其全部父控件的光标
        /// </summary>
        private void SetApplicationCursor(Cursor cursor)
        {
            Cursor = cursor;
            Control parent = Parent;
            while (parent != null)
            {
                parent.Cursor = cursor;
                parent = parent.Parent;
            }
        }

        private void SetStartPosition()
        {
            Value = 0;
            Refresh();
        }

        private void ShowBeginStep()
        {
            Value += 1;
            Refresh();
        }

        private void ShowProgressStep()
        {
            if (Value + 1 < _NowMaxBarValue)
            {
                Value += 1;
            }
        }

        private void ShowFinalStep()
        {
            while (Value + 1 < Maximum)
            {
                Value += 1;
            }
            Value = Maximum;
            Refresh();
        }

        private bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            string realName = Path.GetFileName(fileName);
            string pathName = Path.GetDirectoryName(fileName);


            char[] errChars = Path.GetInvalidPathChars();
            if (realName.IndexOfAny(errChars) >= 0)
            {
                return false;
            }


            errChars = Path.GetInvalidPathChars();
            if (pathName.IndexOfAny(errChars) >= 0)
            {
                return false;
            }
            return true;
        }
    }
}