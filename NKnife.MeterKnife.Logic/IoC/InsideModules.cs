using Autofac;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Base;

namespace NKnife.MeterKnife.Logic.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Logic.Kernel>().As<IGlobal>().SingleInstance();
            builder.RegisterType<AntService>().As<IAntService>().SingleInstance();

            builder.RegisterType<PerformStorageLogic>().As<IPerformStorageLogic>().SingleInstance();
            builder.RegisterType<EngineeringLogic>().As<IEngineeringLogic>().SingleInstance();
        }
    }
}
