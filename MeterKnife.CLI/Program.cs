using System;
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
            builder.RegisterAssemblyModules(typeof(Logic.Global).Assembly);

            builder.RegisterType<SerialCliCommand>().Named<ICommand>("serial").SingleInstance();
            builder.RegisterType<CareConfigCliCommand>().Named<ICommand>("cc").SingleInstance();
            builder.RegisterType<CareVersionCliCommand>().Named<ICommand>("cv").SingleInstance();
            
            builder.RegisterType<CliStartup>().AsSelf().SingleInstance();

            var container = builder.Build();
            var startup = container.Resolve<CliStartup>();
            startup.Initialize();

            await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .UseCommandFactory(schema => container.ResolveNamed<ICommand>(schema.Name))
                .Build()
                .RunAsync(args);

            Console.ReadLine();
        }
    }
}
