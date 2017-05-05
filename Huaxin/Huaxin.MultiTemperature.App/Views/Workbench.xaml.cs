using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;
using Huaxin.MultiTemperature.App.Controls;
using Huaxin.MultiTemperature.App.Dialogs;
using Huaxin.MultiTemperature.App.ViewEntities;
using Huaxin.MultiTemperature.App.ViewModels;
using Huaxin.MultiTemperature.App.Views.SubPages;
using NKnife.IoC;
using OxyPlot;

namespace Huaxin.MultiTemperature.App.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Workbench
    {
        private readonly WorkbenchViewModel _ViewModel;

        public Workbench()
        {
            InitializeComponent();
            _ViewModel = (WorkbenchViewModel) DataContext;
            _ViewModel.PropertyChanged += OnViewModelPropertyChanged;
#if !DEBUG
            this.GoFullscreen();
#endif
            ButtonClickEventManager();
            ControlEventManager();
            ViewModelChangedManager();
            PageContainer.Children.Add(DI.Get<HomePage>());
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_ViewModel.CurrentPage):
                    FillPageContainer(_ViewModel.CurrentPage);
                    break;
            }
        }

        private void FillPageContainer(string currentPage)
        {
            UserControl uie = null;
            switch (currentPage)
            {
                case nameof(HomePage):
                    uie = DI.Get<HomePage>();
                    break;
                case nameof(ProjectAndDatasPage):
                    uie = DI.Get<ProjectAndDatasPage>();
                    break;
                case nameof(MeterPointPage):
                    uie = DI.Get<MeterPointPage>();
                    break;
                case nameof(MeterDatasPage):
                    uie = DI.Get<MeterDatasPage>();
                    break;
                case nameof(RealTimePlotViewPage):
                    uie = DI.Get<RealTimePlotViewPage>();
                    break;
                case nameof(OptionAndToolsPage):
                    uie = DI.Get<OptionAndToolsPage>();
                    break;
                case nameof(ProjectAndMeterInfoPage):
                    uie = DI.Get<ProjectAndMeterInfoPage>();
                    break;
            }
            if (uie != null)
            {
                PageContainer.Children.Clear();
                PageContainer.Children.Add(uie);
            }
        }

        private void ButtonClickEventManager()
        {
            HomeButton.Click += (s, e) => { _ViewModel.CurrentPage = nameof(HomePage); };
            ProjectAndDatasButton.Click += (s, e) => { _ViewModel.CurrentPage = nameof(ProjectAndDatasPage); };
            MeterPointButton.Click += (s, e) => { _ViewModel.CurrentPage = nameof(MeterPointPage); };
            MeterDatasButton.Click += (s, e) => { _ViewModel.CurrentPage = nameof(MeterDatasPage); };
            PlotButton.Click += (s, e) => { _ViewModel.CurrentPage = nameof(RealTimePlotViewPage); };
            OptionAndToolsButton.Click += (s, e) => { _ViewModel.CurrentPage = nameof(OptionAndToolsPage); };
        }

        private void ControlEventManager()
        {
            Loaded += (s, e) =>
            {
                _ViewModel.BuildMeterPoints();
            };
        }

        private void ViewModelChangedManager()
        {
            _ViewModel.MeterPoints.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                    {
                        foreach (MeterPoint point in e.NewItems)
                        {
                            var panel = new MeterPointPanel
                            {
                                Point = point.Point,
                                ComputeValue = point.ComputeValue,
                                MeterValue = point.MeterValue
                            };
                            //PointsPanel.Children.Add(panel);
                            point.PropertyChanged += (o, item) =>
                            {
                                switch (item.PropertyName)
                                {
                                    case nameof(MeterPoint.Point):
                                        panel.Point = point.Point;
                                        break;
                                    case nameof(MeterPoint.ComputeValue):
                                        panel.ComputeValue = point.ComputeValue;
                                        break;
                                    case nameof(MeterPoint.MeterValue):
                                        panel.MeterValue = point.MeterValue;
                                        break;
                                }
                            };
                        }
                        break;
                    }
                    case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (MeterPoint point in e.NewItems)
                        {
                            MeterPointPanel rp = null;
                            bool has = false;
//                            foreach (MeterPointPanel panel in PointsPanel.Children)
//                            {
//                                if (panel.Point == point.Point)
//                                {
//                                    has = true;
//                                    rp = panel;
//                                    break;
//                                }
//                            }
//                            if (has) PointsPanel.Children.Remove(rp);
                        }
                        break;
                    }
                    case NotifyCollectionChangedAction.Move:
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Reset:
                        break;
                }
            };
        }
    }
}
