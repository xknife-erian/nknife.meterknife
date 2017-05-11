using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huaxin.MultiTemperature.App.ViewModels;
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
            RealTimePlotView = DI.Get<RealTimePlotViewViewModel>();
            CompanyAndMeterInfo = DI.Get<CompanyAndMeterInfoViewModel>();
            ProjectAndDatas = DI.Get<ProjectAndDatasViewModel>();
        }
        public WorkbenchViewModel Workbench { get; }
        public RealTimePlotViewViewModel RealTimePlotView { get; private set; }
        public CompanyAndMeterInfoViewModel CompanyAndMeterInfo { get; set; }
        public ProjectAndDatasViewModel ProjectAndDatas { get; set; }
    }
}
