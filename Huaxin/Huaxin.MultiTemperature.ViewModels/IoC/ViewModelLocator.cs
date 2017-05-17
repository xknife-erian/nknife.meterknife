using NKnife.IoC;

namespace Huaxin.MultiTemperature.ViewModels.IoC
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
