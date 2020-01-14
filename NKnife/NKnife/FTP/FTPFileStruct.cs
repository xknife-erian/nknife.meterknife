using System;

namespace NKnife.FTP
{
    public struct FtpFileStruct
    {
        public DateTime CreateTime { get; set; }
        public string Flags { get; set; }
        public string Group { get; set; }
        public bool IsDirectory { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
    }
}