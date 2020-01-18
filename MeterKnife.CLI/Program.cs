using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Autofac;
using CliFx;
using CliFx.Attributes;
using CliFx.Services;
using MeterKnife.CLI.Commands;
using MeterKnife.Util.Serial;
using NKnife.Util;

namespace MeterKnife.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SerialHelper.RefreshSerialPorts();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Program).Assembly);
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
