using System;
using System.IO;
using System.Text;
using MeterKnife.Util.Encrypt;

namespace MeterKnife.Util.Zip
{
    /// <summary>
    ///     File entry class.
    /// </summary>
    public class GZipFileEntry
    {
        private DateTime _CreationTime;
        private int _FileEntryLength;


        private string _FileFullName;
        private int _GZipFileLength;


        private DateTime _LastAccessTime;
        private DateTime _LastWriteTime;
        private int _OriginalLength;


        public GZipFileEntry()
        {
        }


        public GZipFileEntry(string fileName)
        {
            var fileInfo = new FileInfo(fileName);


            _OriginalLength = (int) fileInfo.Length;
            _FileFullName = fileInfo.FullName;
            _CreationTime = fileInfo.CreationTime;
            _LastAccessTime = fileInfo.LastAccessTime;
            _LastWriteTime = fileInfo.LastWriteTime;
        }


        public int OriginalLength
        {
            get { return _OriginalLength; }
            set { _OriginalLength = value; }
        }


        public int GZipFileLength
        {
            get { return _GZipFileLength; }
            set { _GZipFileLength = value; }
        }


        public int FileEntryLength
        {
            get { return _FileEntryLength; }
            set { _FileEntryLength = value; }
        }

        public string FormattedStr
        {
            get
            {
                var sb = new StringBuilder();


                sb.Append(Path.GetFileName(_FileFullName));
                sb.Append("|" + _OriginalLength);
                sb.Append("|" + _GZipFileLength);
                sb.Append("|" + _LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("|" + _LastAccessTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("|" + _CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("|" + _FileFullName);


                return sb.ToString();
            }
        }


        public string FileName
        {
            get { return Path.GetFileName(_FileFullName); }
        }


        public string FileFullName
        {
            get { return _FileFullName; }
        }


        public void WriteLengthInfo(Stream outStream)
        {
            byte[] bytes1 = BitConverter.GetBytes(_OriginalLength);
            byte[] bytes2 = BitConverter.GetBytes(_GZipFileLength);
            byte[] bytes3 = BitConverter.GetBytes(_FileEntryLength);


            SimpleCipher.EncryptBytes(bytes1);
            SimpleCipher.EncryptBytes(bytes2);
            SimpleCipher.EncryptBytes(bytes3);


            outStream.Write(bytes1, 0, bytes1.Length);
            outStream.Write(bytes2, 0, bytes2.Length);
            outStream.Write(bytes3, 0, bytes3.Length);
        }


        public void ReadLengthInfo(Stream srcStream)
        {
            var bytes1 = new byte[4];
            var bytes2 = new byte[4];
            var bytes3 = new byte[4];


            srcStream.Read(bytes1, 0, bytes1.Length);
            srcStream.Read(bytes2, 0, bytes2.Length);
            srcStream.Read(bytes3, 0, bytes3.Length);


            SimpleCipher.EncryptBytes(bytes1);
            SimpleCipher.EncryptBytes(bytes2);
            SimpleCipher.EncryptBytes(bytes3);


            _OriginalLength = BitConverter.ToInt32(bytes1, 0);
            _GZipFileLength = BitConverter.ToInt32(bytes2, 0);
            _FileEntryLength = BitConverter.ToInt32(bytes3, 0);
        }


        public void WriteEntryInfo(Stream outStream)
        {
            byte[] entryBytes = GetFileEntryByes();
            SimpleCipher.EncryptBytes(entryBytes);
            outStream.Write(entryBytes, 0, entryBytes.Length); // 文件项内容


            _FileEntryLength = entryBytes.Length;
        }


        public void ReadEntryInfo(Stream srcStream)
        {
            var entryBytes = new byte[_FileEntryLength];
            srcStream.Read(entryBytes, 0, entryBytes.Length); // FileEntry 字节
            SimpleCipher.EncryptBytes(entryBytes);


            string entryStr = Encoding.Default.GetString(entryBytes); // 不能用 ASCII, 要处理汉字
            string[] strArray = entryStr.Split('|');


            long lastWriteTimeticks = long.Parse(strArray[3]);
            long lastAccessTimeticks = long.Parse(strArray[4]);
            long lastCreateTimeticks = long.Parse(strArray[5]);


            _LastWriteTime = new DateTime(lastWriteTimeticks);
            _LastAccessTime = new DateTime(lastAccessTimeticks);
            _CreationTime = new DateTime(lastCreateTimeticks);


            _FileFullName = strArray[6];
        }


        public void ResetFileDateTime(string folderCompressTo)
        {
            string fileName = folderCompressTo + Path.GetFileName(_FileFullName);


            File.SetLastAccessTime(fileName, _LastAccessTime);
            File.SetCreationTime(fileName, _CreationTime);
            File.SetLastWriteTime(fileName, _LastWriteTime);
        }


        private byte[] GetFileEntryByes()
        {
            var sb = new StringBuilder();


            sb.Append(Path.GetFileName(_FileFullName));
            sb.Append("|" + _OriginalLength);
            sb.Append("|" + _GZipFileLength);
            sb.Append("|" + _LastWriteTime.Ticks);
            sb.Append("|" + _LastAccessTime.Ticks);
            sb.Append("|" + _CreationTime.Ticks);
            sb.Append("|" + _FileFullName);


            string str = sb.ToString();
            return Encoding.Default.GetBytes(str); // 不能用 ASCII, 要处理汉字
        }
    }
}