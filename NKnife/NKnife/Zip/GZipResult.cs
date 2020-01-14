namespace NKnife.Zip
{
    public class GZipResult
    {
        public int CompressionPercent { get; set; }
        public bool Errors { get; set; }
        public int FileCount { get; set; }
        public GZipFileInfo[] Files { get; set; }
        public string TempFile { get; set; }
        public bool TempFileDeleted { get; set; }
        public long TempFileSize { get; set; }
        public string ZipFile { get; set; }
        public long ZipFileSize { get; set; }
    }
}