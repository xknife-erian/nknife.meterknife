namespace MeterKnife.Util.Interface
{
    public interface IFileCompress
    {
        void ZipFiles(string[] files, string targetFilename, string targetDir);
        void UnZipFiles(string fullName, string targetDir);
    }
}
