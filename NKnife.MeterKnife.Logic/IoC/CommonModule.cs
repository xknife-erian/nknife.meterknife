using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;

namespace NKnife.MeterKnife.Logic.IoC
{
    public class CommonModule : Module
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
            builder.RegisterType<PathManager>().As<IPathManager>().SingleInstance();
            builder.RegisterType<HabitManager>().As<IHabitManager>().SingleInstance();
        }

        #endregion
    }
}
