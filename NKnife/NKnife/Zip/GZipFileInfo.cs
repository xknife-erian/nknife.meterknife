using System;

namespace NKnife.Zip
{
    public class GZipFileInfo
    {
        public bool AddedToTempFile { get; set; }
        public string Folder { get; set; }
        public int Index { get; set; }
        public int Length { get; set; }
        public string LocalPath { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RelativePath { get; set; }
        public bool RestoreRequested { get; set; }
        public bool Restored { get; set; }

        public bool ParseFileInfo(string fileInfo)
        {
            bool success = false;
            try
            {
                if (!string.IsNullOrEmpty(fileInfo))
                {
                    // get the file information
                    string[] info = fileInfo.Split(',');
                    if (info.Length == 4)
                    {
                        Index = Convert.ToInt32(info[0]);
                        RelativePath = info[1].Replace("/", "\\");
                        ModifiedDate = Convert.ToDateTime(info[2]);
                        Length = Convert.ToInt32(info[3]);
                        success = true;
                    }
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }
    }
}