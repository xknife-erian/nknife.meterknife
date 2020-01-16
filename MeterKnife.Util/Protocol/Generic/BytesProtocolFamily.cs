using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NKnife.IoC;
using Ninject;

namespace NKnife.Protocol.Generic
{
    [Serializable]
    public class BytesProtocolFamily : IProtocolFamily<byte[]>
    {
        protected Func<byte[], BytesProtocol> _DefaultProtocolBuilder;
        protected Dictionary<byte[], Func<byte[], BytesProtocol>> _ProtocolBuilderMap = new Dictionary<byte[], Func<byte[], BytesProtocol>>();
        protected Func<byte[], BytesProtocolPacker> _DefaultProtocolPackerGetter;
        protected Dictionary<byte[], Func<byte[], BytesProtocolPacker>> _ProtocolPackerGetterMap = new Dictionary<byte[], Func<byte[], BytesProtocolPacker>>();
        protected Func<byte[], BytesProtocolUnPacker> _DefaultProtocolUnPackerGetter;
        protected Dictionary<byte[], Func<byte[], BytesProtocolUnPacker>> _ProtocolUnPackerGetterMap = new Dictionary<byte[], Func<byte[], BytesProtocolUnPacker>>();

        public BytesProtocolFamily()
        {
        }

        public BytesProtocolFamily(string name)
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
                if (!_HasSetCommandParser) //如果没有设，则从DI取
                {
                    try
                    {
                        _CommandParser = string.IsNullOrEmpty(FamilyName)
                            ? DI.Get<BytesProtocolCommandParser>()
                            : DI.Get<BytesProtocolCommandParser>(FamilyName);
                    }
                    catch (ActivationException ex)
                    {
                        _CommandParser = DI.Get<BytesProtocolCommandParser>();
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
            if (_ProtocolBuilderMap.ContainsKey(command))
            {
                result = _ProtocolBuilderMap[command].Invoke(command);
            }
            else 
            {
                result = _DefaultProtocolBuilder == null ? DI.Get<BytesProtocol>() : _DefaultProtocolBuilder.Invoke(command);
            }
            result.Family = FamilyName;
            result.Command = command;
            return result;
        }

        public void AddProtocolBuilder(Func<byte[], BytesProtocol> func)
        {
            _DefaultProtocolBuilder = func;
        }

        public void AddProtocolBuilder(byte[] command, Func<byte[], BytesProtocol> func)
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
                if (_ProtocolUnPackerGetterMap.ContainsKey(command))
                {
                    _ProtocolUnPackerGetterMap[command].Invoke(command).Execute(protocol, datagram, command);
                }
                else
                {
                    if (_DefaultProtocolUnPackerGetter == null)
                    {
                        DI.Get<BytesProtocolUnPacker>().Execute(protocol, datagram, command);
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
        /// 将protocol实例转换成字符串，与Parse方法相对
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public byte[] Generate(BytesProtocol protocol)
        {
            var command = protocol.Command;
            if(_ProtocolPackerGetterMap.ContainsKey(command))
            {
                return _ProtocolPackerGetterMap[command].Invoke(command).Combine(protocol);
            }
            return _DefaultProtocolPackerGetter == null ? 
                DI.Get<BytesProtocolPacker>().Combine(protocol) : 
                _DefaultProtocolPackerGetter.Invoke(command).Combine(protocol);
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
            if (_ProtocolPackerGetterMap.ContainsKey(command))
            {
                return _ProtocolPackerGetterMap[command].Invoke(param).Combine(protocol);
            }
            return _DefaultProtocolPackerGetter == null ?
                DI.Get<BytesProtocolPacker>().Combine(protocol) :
                _DefaultProtocolPackerGetter.Invoke(param).Combine(protocol);
        }

        public void AddPackerGetter(Func<byte[], BytesProtocolPacker> func)
        {
            _DefaultProtocolPackerGetter = func;
        }

        public void AddPackerGetter(byte[] command, Func<byte[], BytesProtocolPacker> func)
        {
            if (_ProtocolPackerGetterMap.ContainsKey(command))
            {
                _ProtocolPackerGetterMap[command] = func;
            }
            else
            {
                _ProtocolPackerGetterMap.Add(command,func);   
            }
        }

        public void AddUnPackerGetter(Func<byte[], BytesProtocolUnPacker> func)
        {
            _DefaultProtocolUnPackerGetter = func;
        }

        public void AddUnPackerGetter(byte[] command, Func<byte[], BytesProtocolUnPacker> func)
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
    }
}
