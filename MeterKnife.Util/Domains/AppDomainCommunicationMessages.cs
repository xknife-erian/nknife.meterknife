using System;

namespace NKnife.Domains
{
    public abstract class AppDomainCommunicationMessages
    {
        /// <summary>服务的域名
        /// </summary>
        public abstract string DomainName { get; set; }
        /// <summary>退出服务的协议
        /// </summary>
        public abstract string Exit { get; set; }
    }
}
