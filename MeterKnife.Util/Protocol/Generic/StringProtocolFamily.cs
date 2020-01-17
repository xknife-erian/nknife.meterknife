using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MeterKnife.Util.Protocol.Generic
{
    /// <summary>
    ///     协议族
    /// </summary>
    [Serializable]
    public class StringProtocolFamily : IProtocolFamily<string>
    {
        private StringProtocolCommandParser _commandParser;
        protected Func<string, StringProtocol> _defaultProtocolBuilder;
        protected Func<string, StringProtocolPacker> _defaultProtocolPackerGetter;
        protected Func<string, StringProtocolUnPacker> _defaultProtocolUnPackerGetter;
        private bool _hasSetCommandParser;

        protected Dictionary<string, Func<string, StringProtocol>> _protocolBuilderMap
            = new Dictionary<string, Func<string, StringProtocol>>();

        protected Dictionary<string, Func<string, StringProtocolPacker>> _protocolPackerGetterMap
            = new Dictionary<string, Func<string, StringProtocolPacker>>();

        protected Dictionary<string, Func<string, StringProtocolUnPacker>> _protocolUnPackerGetterMap
            = new Dictionary<string, Func<string, StringProtocolUnPacker>>();

        private StringProtocol _stringProtocol;
        private StringProtocolPacker _stringProtocolPacker;
        private StringProtocolUnPacker _stringProtocolUnPacker;

        public StringProtocolFamily(StringProtocolCommandParser stringProtocolCommandParser, StringProtocol stringProtocol, StringProtocolPacker stringProtocolPacker,
            StringProtocolUnPacker stringProtocolUnPacker)
        {
            _commandParser = stringProtocolCommandParser;
            _stringProtocol = stringProtocol;
            _stringProtocolPacker = stringProtocolPacker;
            _stringProtocolUnPacker = stringProtocolUnPacker;
        }

        public StringProtocolFamily(string name, StringProtocolCommandParser stringProtocolCommandParser, StringProtocol stringProtocol, StringProtocolPacker stringProtocolPacker,
            StringProtocolUnPacker stringProtocolUnPacker)
            : this(stringProtocolCommandParser, stringProtocol, stringProtocolPacker, stringProtocolUnPacker)
        {
            FamilyName = name;
        }

        public StringProtocolCommandParser CommandParser
        {
            get => _commandParser;
            set
            {
                _commandParser = value;
                _hasSetCommandParser = true;
            }
        }

        public string FamilyName { get; set; }

        public StringProtocol Build(string command)
        {
            Debug.Assert(!string.IsNullOrEmpty(FamilyName), "未设置协议族名称");
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentNullException("command", "协议命令字不能为空");

            StringProtocol result;
            if (_protocolBuilderMap.ContainsKey(command))
                result = _protocolBuilderMap[command].Invoke(command);
            else
                result = _defaultProtocolBuilder == null ? _stringProtocol : _defaultProtocolBuilder.Invoke(command);
            result.Family = FamilyName;
            result.Command = command;
            return result;
        }

        public void AddProtocolBuilder(Func<string, StringProtocol> func)
        {
            _defaultProtocolBuilder = func;
        }

        public void AddProtocolBuilder(string command, Func<string, StringProtocol> func)
        {
            if (_protocolBuilderMap.ContainsKey(command))
                _protocolBuilderMap[command] = func;
            else
                _protocolBuilderMap.Add(command, func);
        }

        /// <summary>
        ///     根据远端得到的数据包解析，将数据填充到本实例中，与Generate方法相对
        /// </summary>
        /// <param name="command"></param>
        /// <param name="datagram">The datas.</param>
        public StringProtocol Parse(string command, string datagram)
        {
            var protocol = Build(command);
            if (string.IsNullOrWhiteSpace(datagram)) Debug.Fail("空数据无法进行协议的解析");
            try
            {
                if (_protocolUnPackerGetterMap.ContainsKey(command))
                {
                    _protocolUnPackerGetterMap[command].Invoke(command).Execute(protocol, datagram, command);
                }
                else
                {
                    if (_defaultProtocolUnPackerGetter == null)
                        _stringProtocolUnPacker.Execute(protocol, datagram, command);
                    else
                        _defaultProtocolUnPackerGetter.Invoke(command).Execute(protocol, datagram, command);
                }
            }
            catch (Exception e)
            {
                Debug.Fail($"协议字符串无法解析.{e.Message}..{datagram}");
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
            if (_protocolPackerGetterMap.ContainsKey(command)) return _protocolPackerGetterMap[command].Invoke(command).Combine(protocol);
            return _defaultProtocolPackerGetter == null
                ? _stringProtocolPacker.Combine(protocol)
                : _defaultProtocolPackerGetter.Invoke(command).Combine(protocol);
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
            if (_protocolPackerGetterMap.ContainsKey(command)) return _protocolPackerGetterMap[command].Invoke(param).Combine(protocol);
            return _defaultProtocolPackerGetter == null
                ? _stringProtocolPacker.Combine(protocol)
                : _defaultProtocolPackerGetter.Invoke(param).Combine(protocol);
        }

        public void AddPackerGetter(Func<string, StringProtocolPacker> func)
        {
            _defaultProtocolPackerGetter = func;
        }

        public void AddPackerGetter(string command, Func<string, StringProtocolPacker> func)
        {
            if (_protocolPackerGetterMap.ContainsKey(command))
                _protocolPackerGetterMap[command] = func;
            else
                _protocolPackerGetterMap.Add(command, func);
        }

        public void AddUnPackerGetter(Func<string, StringProtocolUnPacker> func)
        {
            _defaultProtocolUnPackerGetter = func;
        }

        public void AddUnPackerGetter(string command, Func<string, StringProtocolUnPacker> func)
        {
            if (_protocolUnPackerGetterMap.ContainsKey(command))
                _protocolUnPackerGetterMap[command] = func;
            else
                _protocolUnPackerGetterMap.Add(command, func);
        }

        #region 隐式实现

        IProtocolCommandParser<string> IProtocolFamily<string>.CommandParser
        {
            get => CommandParser;
            set => CommandParser = (StringProtocolCommandParser) value;
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