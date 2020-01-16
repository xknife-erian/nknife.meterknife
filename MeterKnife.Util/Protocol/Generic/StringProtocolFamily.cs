using System;
using System.Collections.Generic;
using System.Diagnostics;
using NKnife.IoC;
using Ninject;

namespace NKnife.Protocol.Generic
{
    /// <summary>
    ///     协议族
    /// </summary>
    [Serializable]
    public class StringProtocolFamily : IProtocolFamily<string>
    {
        private StringProtocolCommandParser _CommandParser;
        protected Func<string, StringProtocol> _DefaultProtocolBuilder;
        protected Func<string, StringProtocolPacker> _DefaultProtocolPackerGetter;
        protected Func<string, StringProtocolUnPacker> _DefaultProtocolUnPackerGetter;
        private bool _HasSetCommandParser;
        protected Dictionary<string, Func<string, StringProtocol>> _ProtocolBuilderMap = new Dictionary<string, Func<string, StringProtocol>>();
        protected Dictionary<string, Func<string, StringProtocolPacker>> _ProtocolPackerGetterMap = new Dictionary<string, Func<string, StringProtocolPacker>>();
        protected Dictionary<string, Func<string, StringProtocolUnPacker>> _ProtocolUnPackerGetterMap = new Dictionary<string, Func<string, StringProtocolUnPacker>>();

        public StringProtocolFamily()
        {
        }

        public StringProtocolFamily(string name)
        {
            FamilyName = name;
        }

        public StringProtocolCommandParser CommandParser
        {
            get
            {
                if (!_HasSetCommandParser)
                {
                    try
                    {
                        _CommandParser = string.IsNullOrEmpty(FamilyName)
                            ? DI.Get<StringProtocolCommandParser>()
                            : DI.Get<StringProtocolCommandParser>(FamilyName);
                    }
                    catch (ActivationException ex)
                    {
                        _CommandParser = DI.Get<StringProtocolCommandParser>();
                    }
                    _HasSetCommandParser = true;
                }
                return _CommandParser;
            }
            set
            {
                _CommandParser = value;
                _HasSetCommandParser = true;
            }
        }

        public string FamilyName { get; set; }

        public StringProtocol Build(string command)
        {
            Debug.Assert(!string.IsNullOrEmpty(FamilyName), "未设置协议族名称");
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentNullException("command", "协议命令字不能为空");

            StringProtocol result;
            if (_ProtocolBuilderMap.ContainsKey(command))
            {
                result = _ProtocolBuilderMap[command].Invoke(command);
            }
            else
            {
                result = _DefaultProtocolBuilder == null ? DI.Get<StringProtocol>() : _DefaultProtocolBuilder.Invoke(command);
            }
            result.Family = FamilyName;
            result.Command = command;
            return result;
        }

        public void AddProtocolBuilder(Func<string, StringProtocol> func)
        {
            _DefaultProtocolBuilder = func;
        }

        public void AddProtocolBuilder(string command, Func<string, StringProtocol> func)
        {
            if (_ProtocolBuilderMap.ContainsKey(command))
            {
                _ProtocolBuilderMap[command] = func;
            }
            else
            {
                _ProtocolBuilderMap.Add(command, func);
            }
        }

        /// <summary>
        ///     根据远端得到的数据包解析，将数据填充到本实例中，与Generate方法相对
        /// </summary>
        /// <param name="command"></param>
        /// <param name="datagram">The datas.</param>
        public StringProtocol Parse(string command, string datagram)
        {
            var protocol = Build(command);
            if (string.IsNullOrWhiteSpace(datagram))
            {
                Debug.Fail("空数据无法进行协议的解析");
            }
            try
            {
                if (_ProtocolUnPackerGetterMap.ContainsKey(command))
                {
                    _ProtocolUnPackerGetterMap[command].Invoke(command).Execute(protocol, datagram, command);
                }
                else
                {
                    if (_DefaultProtocolUnPackerGetter == null)
                    {
                        DI.Get<StringProtocolUnPacker>().Execute(protocol, datagram, command);
                    }
                    else
                    {
                        _DefaultProtocolUnPackerGetter.Invoke(command).Execute(protocol, datagram, command);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Fail(string.Format("协议字符串无法解析.{0}..{1}", e.Message, datagram));
            }
            return protocol;
        }

        /// <summary>
        ///     将protocol实例转换成字符串，与Parse方法相对
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public string Generate(StringProtocol protocol)
        {
            var command = protocol.Command;
            if (_ProtocolPackerGetterMap.ContainsKey(command))
            {
                return _ProtocolPackerGetterMap[command].Invoke(command).Combine(protocol);
            }
            return _DefaultProtocolPackerGetter == null
                ? DI.Get<StringProtocolPacker>().Combine(protocol)
                : _DefaultProtocolPackerGetter.Invoke(command).Combine(protocol);
        }

        /// <summary>
        ///     将protocol实例转换成字符串，与Parse方法相对
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="param">PackerGetter的参数，默认使用command作为参数</param>
        /// <returns></returns>
        public string Generate(StringProtocol protocol, string param)
        {
            var command = protocol.Command;
            if (_ProtocolPackerGetterMap.ContainsKey(command))
            {
                return _ProtocolPackerGetterMap[command].Invoke(param).Combine(protocol);
            }
            return _DefaultProtocolPackerGetter == null
                ? DI.Get<StringProtocolPacker>().Combine(protocol)
                : _DefaultProtocolPackerGetter.Invoke(param).Combine(protocol);
        }

        public void AddPackerGetter(Func<string, StringProtocolPacker> func)
        {
            _DefaultProtocolPackerGetter = func;
        }

        public void AddPackerGetter(string command, Func<string, StringProtocolPacker> func)
        {
            if (_ProtocolPackerGetterMap.ContainsKey(command))
            {
                _ProtocolPackerGetterMap[command] = func;
            }
            else
            {
                _ProtocolPackerGetterMap.Add(command, func);
            }
        }

        public void AddUnPackerGetter(Func<string, StringProtocolUnPacker> func)
        {
            _DefaultProtocolUnPackerGetter = func;
        }

        public void AddUnPackerGetter(string command, Func<string, StringProtocolUnPacker> func)
        {
            if (_ProtocolUnPackerGetterMap.ContainsKey(command))
            {
                _ProtocolUnPackerGetterMap[command] = func;
            }
            else
            {
                _ProtocolUnPackerGetterMap.Add(command, func);
            }
        }

        #region 隐式实现

        IProtocolCommandParser<string> IProtocolFamily<string>.CommandParser
        {
            get { return CommandParser; }
            set { CommandParser = (StringProtocolCommandParser) value; }
        }

        IProtocol<string> IProtocolFamily<string>.Build(string command)
        {
            return Build(command);
        }

        IProtocol<string> IProtocolFamily<string>.Parse(string command, string datagram)
        {
            return Parse(command, datagram);
        }

        string IProtocolFamily<string>.Generate(IProtocol<string> protocol)
        {
            return Generate((StringProtocol) protocol);
        }

        void IProtocolFamily<string>.AddPackerGetter(Func<string, IProtocolPacker<string>> func)
        {
            AddPackerGetter((Func<string, StringProtocolPacker>) func);
        }

        void IProtocolFamily<string>.AddPackerGetter(string command, Func<string, IProtocolPacker<string>> func)
        {
            AddPackerGetter(command, (Func<string, StringProtocolPacker>) func);
        }

        #endregion
    }
}