using Autofac;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Logic.Services;

namespace NKnife.MeterKnife.Logic.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Logic.Global>().As<IGlobal>().SingleInstance();
            builder.RegisterType<PerformStorageLogic>().As<IPerformStorageLogic>().SingleInstance();

            builder.RegisterType<AntService>().As<ISlotService>();
            builder.RegisterType<CareTemperatureGetter>().As<ITemperatureGetter>().SingleInstance();
        }
    }
}
