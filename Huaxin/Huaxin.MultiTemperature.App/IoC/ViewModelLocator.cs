using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huaxin.MultiTemperature.App.Views;
using Ninject;
using NKnife.IoC;

namespace Huaxin.MultiTemperature.App.IoC
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            Workbench = DI.Get<WorkbenchViewModel>();
        }
        public WorkbenchViewModel Workbench { get; }
    }
}
