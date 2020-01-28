using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Storage.Base;
using NKnife.MeterKnife.Storage.Db;

namespace NKnife.MeterKnife.Logic.IoC
{
    public class StorageModules : Module
    {
        #region Overrides of Module

        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EngineeringFileBuilder>().AsSelf().SingleInstance();
            builder.RegisterType<StorageManager>().As<IStorageManager>().SingleInstance();

            builder.RegisterGeneric(typeof(BaseStoragePlatform<>)).As(typeof(IStoragePlatform<>)).SingleInstance();
            builder.RegisterGeneric(typeof(BaseStorageDUTRead<>)).As(typeof(IStorageDUTRead<>)).SingleInstance();
            builder.RegisterGeneric(typeof(BaseStorageDUTWrite<>)).As(typeof(IStorageDUTWrite<>)).SingleInstance();

            var assembly = typeof(StorageManager).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && (t.Name.Contains("Storage")))
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        #endregion
    }
}
