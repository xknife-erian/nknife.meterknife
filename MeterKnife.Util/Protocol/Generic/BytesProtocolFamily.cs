using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NKnife.MeterKnife.Util.Protocol.Generic
{
    [Serializable]
    public class BytesProtocolFamily : IProtocolFamily<byte[]>
    {
        protected Func<byte[], BytesProtocol> _defaultProtocolBuilder;
        protected Dictionary<byte[], Func<byte[], BytesProtocol>> _protocolBuilderMap = new Dictionary<byte[], Func<byte[], BytesProtocol>>();
        protected Func<byte[], BytesProtocolPacker> _defaultProtocolPackerGetter;
        protected Dictionary<byte[], Func<byte[], BytesProtocolPacker>> _protocolPackerGetterMap = new Dictionary<byte[], Func<byte[], BytesProtocolPacker>>();
        protected Func<byte[], BytesProtocolUnPacker> _defaultProtocolUnPackerGetter;
        protected Dictionary<byte[], Func<byte[], BytesProtocolUnPacker>> _protocolUnPackerGetterMap = new Dictionary<byte[], Func<byte[], BytesProtocolUnPacker>>();

        private BytesProtocolCommandParser _bytesProtocolCommandParser;
        private BytesProtocol _bytesProtocol;
        private BytesProtocolUnPacker _bytesProtocolUnPacker;
        private BytesProtocolPacker _bytesProtocolPacker;

        public BytesProtocolFamily(BytesProtocolCommandParser bytesProtocolCommandParser, BytesProtocol bytesProtocol, BytesProtocolUnPacker bytesProtocolUnPacker, BytesProtocolPacker bytesProtocolPacker)
        {
            _bytesProtocolCommandParser = bytesProtocolCommandParser;
            _bytesProtocol = bytesProtocol;
            _bytesProtocolUnPacker = bytesProtocolUnPacker;
            _bytesProtocolPacker = bytesProtocolPacker;
        }

        public BytesProtocolFamily(string name, 
            BytesProtocolCommandParser bytesProtocolCommandParser, BytesProtocol bytesProtocol, BytesProtocolUnPacker bytesProtocolUnPacker, BytesProtocolPacker bytesProtocolPacker)
            : this(bytesProtocolCommandParser, bytesProtocol, bytesProtocolUnPacker, bytesProtocolPacker)
        {
            FamilyName = name;
        }

        public string FamilyName { get; set; }

        private BytesProtocolCommandParser _CommandParser;
        private bool _HasSetCommandParser;
        public BytesProtocolCommandParser CommandParser
        {
            get
            {
//                if (!_HasSetCommandParser) //如果没有设，则从DI取
//                {
//                    try
//                    {
//                        _CommandParser = string.IsNullOrEmpty(FamilyName)
//                            ? _bytesProtocolCommandParser
//                            : DI.Get<BytesProtocolCommandParser>(FamilyName);
//                    }
//                    catch (Exception ex)
//                    {
//                        _CommandParser = DI.Get<BytesProtocolCommandParser>();
//                    }
//                    _HasSetCommandParser = true;
//                }
                return _bytesProtocolCommandParser;
            }
            set
            {
                _CommandParser = value;
                _HasSetCommandParser = true;
            }
        }


        #region 隐式实现
        IProtocolCommandParser<byte[]> IProtocolFamily<byte[]>.CommandParser
        {
            get { return CommandParser; }
            set { CommandParser = (BytesProtocolCommandParser)value; }
        }

        IProtocol<byte[]> IProtocolFamily<byte[]>.Build(byte[] command)
        {
            return Build(command);
        }

        IProtocol<byte[]> IProtocolFamily<byte[]>.Parse(byte[] command, byte[] datagram)
        {
            return Parse(command, datagram);
        }

        byte[] IProtocolFamily<byte[]>.Generate(IProtocol<byte[]> protocol)
        {
            return Generate((BytesProtocol)protocol);
        }

        void IProtocolFamily<byte[]>.AddPackerGetter(Func<byte[], IProtocolPacker<byte[]>> func)
        {
            AddPackerGetter((Func<byte[], BytesProtocolPacker>)func);
        }

        void IProtocolFamily<byte[]>.AddPackerGetter(byte[] command, Func<byte[], IProtocolPacker<byte[]>> func)
        {
            AddPackerGetter(command, (Func<byte[], BytesProtocolPacker>)func);
        }
       
        #endregion
        public BytesProtocol Build(byte[] command)
        {
            Debug.Assert(!string.IsNullOrEmpty(FamilyName), "未设置协议族名称");
            if (command == null)
                throw new ArgumentNullException("command", "协议命令字不能为null");
            if(command.Count() ==0)
                throw new ArgumentNullException("command", "协议命令字不能为空");
            BytesProtocol result;
            if (_protocolBuilderMap.ContainsKey(command))
            {
                result = _protocolBuilderMap[command].Invoke(command);
            }
            else 
            {
                result = _defaultProtocolBuilder == null ? _bytesProtocol : _defaultProtocolBuilder.Invoke(command);
            }
            result.Family = FamilyName;
            result.Command = command;
            return result;
        }

        public void AddProtocolBuilder(Func<byte[], BytesProtocol> func)
        {
            _defaultProtocolBuilder = func;
        }

        public void AddProtocolBuilder(byte[] command, Func<byte[], BytesProtocol> func)
        {
            if (_protocolBuilderMap.ContainsKey(command))
            {
                _protocolBuilderMap[command] = func;
            }
            else
            {
                _protocolBuilderMap.Add(command, func);
            }
        }


        /// <summary>
        /// 根据远端得到的数据包解析，将数据填充到本实例中，与Generate方法相对
        /// </summary>
        /// <param name="command"></param>
        /// <param name="datagram">The datas.</param>
        public BytesProtocol Parse(byte[] command, byte[] datagram)
        {
            var protocol = Build(command);
            if (datagram == null)
            {
                Debug.Fail("空数据无法进行协议的解析");
            }
            if (datagram.Count() == 0)
            {
                Debug.Fail("空数据无法进行协议的解析");
            }
            try
            {
                if (_protocolUnPackerGetterMap.ContainsKey(command))
                {
                    _protocolUnPackerGetterMap[command].Invoke(command).Execute(protocol, datagram, command);
                }
                else
                {
                    if (_defaultProtocolUnPackerGetter == null)
                    {
                        _bytesProtocolUnPacker.Execute(protocol, datagram, command);
                    }
                    else
                    {
                        _defaultProtocolUnPackerGetter.Invoke(command).Execute(protocol, datagram, command);
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
        /// 将protocol实例转换成字符串，与Parse方法相对
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public byte[] Generate(BytesProtocol protocol)
        {
            var command = protocol.Command;
            if(_protocolPackerGetterMap.ContainsKey(command))
            {
                return _protocolPackerGetterMap[command].Invoke(command).Combine(protocol);
            }
            return _defaultProtocolPackerGetter == null ? 
                _bytesProtocolPacker.Combine(protocol) : 
                _defaultProtocolPackerGetter.Invoke(command).Combine(protocol);
        }

        /// <summary>
        /// 将protocol实例转换成字符串，与Parse方法相对
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="param">PackerGetter的参数，默认使用command作为参数</param>
        /// <returns></returns>
        public byte[] Generate(BytesProtocol protocol,byte[] param)
        {
            var command = protocol.Command;
            if (_protocolPackerGetterMap.ContainsKey(command))
            {
                return _protocolPackerGetterMap[command].Invoke(param).Combine(protocol);
            }
            return _defaultProtocolPackerGetter == null ?
                _bytesProtocolPacker.Combine(protocol) :
                _defaultProtocolPackerGetter.Invoke(param).Combine(protocol);
        }

        public void AddPackerGetter(Func<byte[], BytesProtocolPacker> func)
        {
            _defaultProtocolPackerGetter = func;
        }

        public void AddPackerGetter(byte[] command, Func<byte[], BytesProtocolPacker> func)
        {
            if (_protocolPackerGetterMap.ContainsKey(command))
            {
                _protocolPackerGetterMap[command] = func;
            }
            else
            {
                _protocolPackerGetterMap.Add(command,func);   
            }
        }

        public void AddUnPackerGetter(Func<byte[], BytesProtocolUnPacker> func)
        {
            _defaultProtocolUnPackerGetter = func;
        }

        public void AddUnPackerGetter(byte[] command, Func<byte[], BytesProtocolUnPacker> func)
        {
            if (_protocolUnPackerGetterMap.ContainsKey(command))
            {
                _protocolUnPackerGetterMap[command] = func;
            }
            else
            {
                _protocolUnPackerGetterMap.Add(command, func);
            }
        }
    }
}
