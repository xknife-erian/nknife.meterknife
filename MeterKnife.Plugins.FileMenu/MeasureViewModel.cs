using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using MeterKnife.Events;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.FileMenu
{
    public class MeasureViewModel : ViewModelBase
    {
        private IExtenderProvider _ExtenderProvider;

        public void SetProvider(IExtenderProvider provider)
        {
            _ExtenderProvider = provider;
        }
    }
}
