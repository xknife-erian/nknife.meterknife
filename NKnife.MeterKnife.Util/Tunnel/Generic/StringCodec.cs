
namespace NKnife.MeterKnife.Util.Tunnel.Generic
{
    public class StringCodec : ITunnelCodec<string>
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private bool _HasSetDecoder;
        private bool _HasSetEncoder;
        private StringDatagramDecoder _stringDecoder;
        private StringDatagramEncoder _stringEncoder;

        public StringCodec(StringDatagramDecoder stringDecoder,StringDatagramEncoder stringEncoder)
        {
            _stringDecoder = stringDecoder;
            _stringEncoder = stringEncoder;
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
//                if (!_HasSetDecoder)
//                {
//                    _stringDecoder = string.IsNullOrEmpty(CodecName) ? DI.Get<StringDatagramDecoder>() : DI.Get<StringDatagramDecoder>(CodecName);
//                    _HasSetDecoder = true;
//                    return _stringDecoder;
//                }
                return _stringDecoder;
            }
            set
            {
                _stringDecoder = value;
                _HasSetDecoder = true;
            }
        }

        public virtual StringDatagramEncoder StringEncoder
        {
            get
            {
//                if (!_HasSetEncoder)
//                {
//                    _stringEncoder = string.IsNullOrEmpty(CodecName) ? DI.Get<StringDatagramEncoder>() : DI.Get<StringDatagramEncoder>(CodecName);
//                    _HasSetEncoder = true;
//                    return _stringEncoder;
//                }
                return _stringEncoder;
            }
            set
            {
                _stringEncoder = value;
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