using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using CliFx;
using Microsoft.Extensions.Configuration;
using NKnife.MeterKnife.CLI.Commands;
using NKnife.MeterKnife.Util.Serial;

namespace NKnife.MeterKnife.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SerialHelper.RefreshSerialPorts();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // ReSharper disable once StringLiteralTypo
                .AddJsonFile("appsettings.json");
            var configModule = new ConfigurationModule(configuration.Build());

            var builder = new ContainerBuilder();
            builder.RegisterModule(configModule);
            builder.RegisterAssemblyModules(typeof(Logic.Global).Assembly);

            builder.RegisterType<SerialCliCommand>().Named<ICommand>("serial").SingleInstance();
            builder.RegisterType<CareConfigCliCommand>().Named<ICommand>("cc").SingleInstance();
            builder.RegisterType<CareCliCommand>().Named<ICommand>("ci").SingleInstance();
            
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
