using System.Threading.Tasks;
using Autofac;
using CliFx;
using NKnife.MeterKnife.CLI.Commands;
using NKnife.MeterKnife.Util.Serial;

namespace NKnife.MeterKnife.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SerialHelper.RefreshSerialPorts();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Program).Assembly);

            builder.RegisterType<CareOneCliCommand>().AsSelf().SingleInstance();
            builder.RegisterType<CliStartup>();

            var container = builder.Build();
            var startup = container.Resolve<CliStartup>();
            startup.Initialize();

            await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .UseCommandFactory(schema => container.Resolve<CareOneCliCommand>() as ICommand)
                .Build()
                .RunAsync(args);
        }
    }
}
