using Ninject.Activation;
using Ninject.Modules;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Generic;
using SerialKnife.Common;
using SerialKnife.Generic.Tools;
using SerialKnife.Interfaces;
using SerialKnife.Wrappers;

namespace SerialKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ISerialPortWrapper>().To<SerialPortWrapperDotNet>().Named(SerialType.DotNet.ToString());
            Bind<ISerialPortWrapper>().To<SerialPortWrapperWinApi>().Named(SerialType.WinApi.ToString());

//            Bind<BytesProtocolCommandParser>().To<FirstByteCommandParser>().When(Request).InSingletonScope();
//
//            Bind<BytesDatagramDecoder>().To<PanFixByteHeadTailDatagramDecoder>().When(Request).InSingletonScope();
//            Bind<BytesDatagramEncoder>().To<PanFixByteHeadTailDatagramEncoder>().When(Request).InSingletonScope();
//
//            Bind<BytesProtocolPacker>().To<PanBytesProtocolSimplePacker>().When(Request).InSingletonScope();
//            Bind<BytesProtocolUnPacker>().To<PanBytesProtocolSimpleUnPacker>().When(Request).InSingletonScope();
        }
        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}
