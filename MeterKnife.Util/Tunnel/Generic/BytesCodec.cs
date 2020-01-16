using Common.Logging;
using NKnife.IoC;

namespace NKnife.Tunnel.Generic
{
    public class BytesCodec : ITunnelCodec<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<BytesCodec>();
        private BytesDatagramDecoder _BytesDecoder;
        private BytesDatagramEncoder _BytesEncoder;
        private bool _HasSetDecoder;
        private bool _HasSetEncoder;

        public BytesCodec()
        {
        }

        public BytesCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        public virtual BytesDatagramDecoder BytesDecoder
        {
            get
            {
                if (!_HasSetDecoder)
                {
                    _BytesDecoder = string.IsNullOrEmpty(CodecName)
                        ? DI.Get<BytesDatagramDecoder>()
                        : DI.Get<BytesDatagramDecoder>(CodecName);
                    _HasSetDecoder = true;
                    return _BytesDecoder;
                }
                return _BytesDecoder;
            }
            set
            {
                _BytesDecoder = value;
                _HasSetDecoder = true;
            }
        }

        public virtual BytesDatagramEncoder BytesEncoder
        {
            get
            {
                if (!_HasSetEncoder)
                {
                    _BytesEncoder = string.IsNullOrEmpty(CodecName)
                        ? DI.Get<BytesDatagramEncoder>()
                        : DI.Get<BytesDatagramEncoder>(CodecName);
                    _HasSetEncoder = true;
                    return _BytesEncoder;
                }
                return _BytesEncoder;
            }
            set
            {
                _BytesEncoder = value;
                _HasSetEncoder = true;
            }
        }

        IDatagramDecoder<byte[]> ITunnelCodec<byte[]>.Decoder
        {
            get { return BytesDecoder; }
            set
            {
                BytesDecoder = (BytesDatagramDecoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<byte[]> ITunnelCodec<byte[]>.Encoder
        {
            get { return BytesEncoder; }
            set
            {
                BytesEncoder = (BytesDatagramEncoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}