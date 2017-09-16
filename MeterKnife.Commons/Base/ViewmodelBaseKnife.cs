using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using NKnife.IoC;

namespace MeterKnife.Base
{
    public class ViewmodelBaseKnife : ViewModelBase
    {
        protected IHabited Habited { get; }
        protected IExtenderProvider ExtenderProvider { get; set; }

        public ViewmodelBaseKnife()
        {
            Habited = DI.Get<IHabited>();
        }

        public virtual void SetProvider(IExtenderProvider extenderProvider)
        {
            ExtenderProvider = extenderProvider;
        }
    }
}
