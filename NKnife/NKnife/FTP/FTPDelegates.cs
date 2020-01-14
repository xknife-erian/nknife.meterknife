using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace NKnife.FTP
{
    public delegate void FtpDownloadDataCompletedDelegate(object sender, AsyncCompletedEventArgs e);

    public delegate void FtpDownloadProgressChangedDelegate(object sender, DownloadProgressChangedEventArgs e);

    public delegate void FtpUploadFileCompletedDelegate(object sender, UploadFileCompletedEventArgs e);

    public delegate void FtpUploadProgressChangedDelegate(object sender, UploadProgressChangedEventArgs e);

}
