using Common.Logging;
using NKnife.IoC;

namespace NKnife.Tunnel.Generic
{
    public class StringCodec : ITunnelCodec<string>
    {
        private static readonly ILog _logger = LogManager.GetLogger<StringCodec>();
        private bool _HasSetDecoder;
        private bool _HasSetEncoder;
        private StringDatagramDecoder _StringDecoder;
        private StringDatagramEncoder _StringEncoder;

        public StringCodec()
        {
        }

        public StringCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        public virtual StringDatagramDecoder StringDecoder
        {
            get
            {
                if (!_HasSetDecoder)
                {
                    _StringDecoder = string.IsNullOrEmpty(CodecName) ? DI.Get<StringDatagramDecoder>() : DI.Get<StringDatagramDecoder>(CodecName);
                    _HasSetDecoder = true;
                    return _StringDecoder;
                }
                return _StringDecoder;
            }
            set
            {
                _StringDecoder = value;
                _HasSetDecoder = true;
            }
        }

        public virtual StringDatagramEncoder StringEncoder
        {
            get
            {
                if (!_HasSetEncoder)
                {
                    _StringEncoder = string.IsNullOrEmpty(CodecName) ? DI.Get<StringDatagramEncoder>() : DI.Get<StringDatagramEncoder>(CodecName);
                    _HasSetEncoder = true;
                    return _StringEncoder;
                }
                return _StringEncoder;
            }
            set
            {
                _StringEncoder = value;
                _HasSetEncoder = true;
            }
        }

        IDatagramDecoder<string> ITunnelCodec<string>.Decoder
        {
            get { return StringDecoder; }
            set
            {
                StringDecoder = (StringDatagramDecoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<string> ITunnelCodec<string>.Encoder
        {
            get { return StringEncoder; }
            set
            {
                StringEncoder = (StringDatagramEncoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}