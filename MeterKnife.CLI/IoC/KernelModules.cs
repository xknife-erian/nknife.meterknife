using Autofac;
using MeterKnife.Common;
using MeterKnife.Kernel;
using MeterKnife.Kernel.Services;

namespace MeterKnife.CLI.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MeterKernel>().As<IMeterKernel>().SingleInstance();
            builder.RegisterType<CareTemperatureService>().As<ITemperatureService>().SingleInstance();
            builder.RegisterType<CareCommService>().As<BaseAntCommService>();
        }
    }
}
