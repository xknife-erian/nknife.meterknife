using Autofac;
using NKnife.Serial.Common;
using NKnife.Serial.Interfaces;
using NKnife.Serial.Wrappers;

namespace NKnife.Serial.IoC
{
    public class DefaultModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ISerialPortWrapper>().Named<SerialPortWrapperDotNet>(SerialType.DotNet.ToString());
            builder.RegisterType<ISerialPortWrapper>().Named<SerialPortWrapperWinApi>(SerialType.WinApi.ToString());

//            Bind<BytesProtocolCommandParser>().To<FirstByteCommandParser>().When(Request).InSingletonScope();
//
//            Bind<BytesDatagramDecoder>().To<PanFixByteHeadTailDatagramDecoder>().When(Request).InSingletonScope();
//            Bind<BytesDatagramEncoder>().To<PanFixByteHeadTailDatagramEncoder>().When(Request).InSingletonScope();
//
//            Bind<BytesProtocolPacker>().To<PanBytesProtocolSimplePacker>().When(Request).InSingletonScope();
//            Bind<BytesProtocolUnPacker>().To<PanBytesProtocolSimpleUnPacker>().When(Request).InSingletonScope();
        }
    }
}
