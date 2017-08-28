using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Base
{
    public class ViewmodelBaseKnife : ViewModelBase
    {
        protected IExtenderProvider ExtenderProvider { get; set; }

        public virtual void SetProvider(IExtenderProvider extenderProvider)
        {
            ExtenderProvider = extenderProvider;
        }
    }
}
