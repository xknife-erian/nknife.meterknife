using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using NKnife.MeterKnife.Common;
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
            base.Load(builder);
            builder.RegisterType<DbService>().As<IDbService>().SingleInstance();
        }

        #endregion
    }
}
