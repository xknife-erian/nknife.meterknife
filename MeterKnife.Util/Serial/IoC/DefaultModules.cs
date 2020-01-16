using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Serial.Interfaces;
using MeterKnife.Util.Serial.Wrappers;
using Ninject.Activation;
using Ninject.Modules;

namespace MeterKnife.Util.Serial.IoC
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
