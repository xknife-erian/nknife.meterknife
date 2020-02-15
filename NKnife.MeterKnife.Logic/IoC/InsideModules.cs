using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Holistic;

namespace NKnife.MeterKnife.Logic.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Kernel>().As<IGlobal>().SingleInstance();
            builder.RegisterType<AntService>().As<IAntService>().SingleInstance();
            builder.RegisterType<AntCollectService>().As<IAntCollectService>().SingleInstance();

            var assembly = this.GetType().Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && t.Name.EndsWith("Logic"))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
        }
    }
}
