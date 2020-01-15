using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Interface
{
    public interface IFileCompress
    {
        void ZipFiles(string[] files, string targetFilename, string targetDir);
        void UnZipFiles(string fullName, string targetDir);
    }
}
