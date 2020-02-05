using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CliFx;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NKnife.MeterKnife.CLI.Commands;
using NKnife.MeterKnife.Storage.Db;
using NKnife.MeterKnife.Util.Serial;

namespace NKnife.MeterKnife.CLI
{
    class Program
    {

        static async Task Main(string[] args)
        {
            SerialHelper.RefreshSerialPorts();

            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions().Configure<StorageSetting>(conf.GetSection(nameof(StorageSetting)));

            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection);
            builder.RegisterAssemblyModules(typeof(Logic.Global).Assembly);

            builder.RegisterType<SerialCliCommand>().Named<ICommand>(nameof(SerialCliCommand)).SingleInstance();
            builder.RegisterType<CareConfigCliCommand>().Named<ICommand>(nameof(CareConfigCliCommand)).SingleInstance();
            builder.RegisterType<CareCliCommand>().Named<ICommand>(nameof(CareCliCommand)).SingleInstance();
            builder.RegisterType<DataCommand>().Named<ICommand>(nameof(DataCommand)).SingleInstance();
            builder.RegisterType<SqlBuildCommand>().Named<ICommand>(nameof(SqlBuildCommand)).SingleInstance();

            var container = builder.Build();

            await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .UseTypeActivator(type => container.ResolveNamed<ICommand>(type.Name))
                .Build()
                .RunAsync(args);

            Console.ReadLine();
        }

    }
}


