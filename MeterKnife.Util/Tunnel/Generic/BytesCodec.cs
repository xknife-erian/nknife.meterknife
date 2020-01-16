
namespace MeterKnife.Util.Tunnel.Generic
{
    public class BytesCodec : ITunnelCodec<byte[]>
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private BytesDatagramDecoder _bytesDecoder;
        private BytesDatagramEncoder _bytesEncoder;
        private bool _HasSetDecoder;
        private bool _HasSetEncoder;

        public BytesCodec(BytesDatagramDecoder decoder, BytesDatagramEncoder encoder)
        {
            _bytesDecoder = decoder;
            _bytesEncoder = encoder;
        }

        public BytesCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        public virtual BytesDatagramDecoder BytesDecoder
        {
            get => _bytesDecoder;
            set
            {
                _bytesDecoder = value;
                _HasSetDecoder = true;
            }
        }

        public virtual BytesDatagramEncoder BytesEncoder
        {
            get
            {
                if (!_HasSetEncoder)
                    //                    _bytesEncoder = string.IsNullOrEmpty(CodecName)
//                        ? DI.Get<BytesDatagramEncoder>()
//                        : DI.Get<BytesDatagramEncoder>(CodecName);
//                    _HasSetEncoder = true;
                    return _bytesEncoder;
                return _bytesEncoder;
            }
            set
            {
                _bytesEncoder = value;
                _HasSetEncoder = true;
            }
        }

        IDatagramDecoder<byte[]> ITunnelCodec<byte[]>.Decoder
        {
            get => BytesDecoder;
            set
            {
                BytesDecoder = (BytesDatagramDecoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<byte[]> ITunnelCodec<byte[]>.Encoder
        {
            get => BytesEncoder;
            set
            {
                BytesEncoder = (BytesDatagramEncoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}