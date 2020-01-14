using System;
using System.Net;

namespace NKnife.FTP
{
    /// <summary>重载WebClient，支持FTP进度
    /// </summary>
    public class FtpWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            var req = (FtpWebRequest)base.GetWebRequest(address);
            if (req != null)
                req.UsePassive = false;
            return req;
        }
    }
}