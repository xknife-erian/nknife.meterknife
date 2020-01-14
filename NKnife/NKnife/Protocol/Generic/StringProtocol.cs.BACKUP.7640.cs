using System;
using System.Diagnostics;
using Ninject;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.IoC;
<<<<<<< HEAD:NKnife.Socket/Generic/KnifeSocketProtocol.cs
using NKnife.Protocol;
=======
>>>>>>> 31ca514b829eaa3b6cf63e92716f300664343184:NKnife/Protocol/Generic/StringProtocol.cs

namespace NKnife.Protocol.Generic
{
    /// <summary>协议的抽象实现
    /// </summary>
    public class StringProtocol : IProtocol<string>
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        [Inject]
        public virtual StringProtocolPacker SocketPacker { get; set; }

        [Inject]
        public virtual StringProtocolUnPacker SocketUnPacker { get; set; }

        [Inject]
        public StringProtocolContent Content { get; set; }

        public StringProtocol()
        {
        }

        public StringProtocol(string family, string command)
        {
            Family = family;
            Command = command;
        }

        #region IProtocol Members

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; set; }

        public string Command { get; set; }

        IProtocolPacker<string> IProtocol<string>.Packer
        {
            get { return SocketPacker; }
            set { SocketPacker = (StringProtocolPacker) value; }
        }

        IProtocolUnPacker<string> IProtocol<string>.UnPacker
        {
            get { return SocketUnPacker; }
            set { SocketUnPacker = (StringProtocolUnPacker) value; }
        }

        /// <summary>协议的具体内容的容器
        /// </summary>
        IProtocolContent<string> IProtocol<string>.Content
        {
            get { return Content; }
            set { Content = (StringProtocolContent) value; }
        }

        /// <summary>
        /// 根据当前实例生成协议的原生字符串表达
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            if (Content != null)
                return SocketPacker.Combine(Content);
            return string.Empty;
        }

        /// <summary>
        /// 根据远端得到的数据包解析，将数据填充到本实例中
        /// </summary>
        /// <param name="datagram">The datas.</param>
        public void Parse(string datagram)
        {
            if (string.IsNullOrWhiteSpace(datagram))
            {
                Debug.Fail("空数据无法进行协议的解析");
                return;
            }
            try
            {
                StringProtocolContent c = Content;
                SocketUnPacker.Execute(c, datagram, Family, Command);
            }
            catch (Exception e)
            {
                _logger.Error(() => string.Format("协议字符串无法解析.{0}..{1}", e.Message, datagram));
            }
        }

        public StringProtocol NewInstance()
        {
            return Build(Family, Command);
        }

        IProtocol<string> IProtocol<string>.NewInstance()
        {
            return NewInstance();
        }

        public override string ToString()
        {
            string str = string.Empty;
            try
            {
                str = Generate();
            }
            catch (Exception e)
            {
                _logger.Warn(() => string.Format("调用协议的ToString方法联动Generate()方法时异常。{0}", e.Message));
            }
            return str;
        }

        #endregion

        public static StringProtocol Build(string family, string command)
        {
            var protocol = DI.Get<StringProtocol>();
            protocol.Family = family;
            protocol.Command = command;
            return protocol;
        }
    }
}