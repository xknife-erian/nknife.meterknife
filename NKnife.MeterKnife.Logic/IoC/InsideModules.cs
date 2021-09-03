using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Holistic;

namespace NKnife.MeterKnife.Logic.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Kernel>().As<IKernel>().SingleInstance();

            var assembly = this.GetType().Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            assembly = this.GetType().Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && t.Name.EndsWith("Logic"))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
        }
    }
}
