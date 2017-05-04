using System.Collections.Specialized;
using Huaxin.MultiTemperature.App.Controls;
using Huaxin.MultiTemperature.App.Dialogs;
using Huaxin.MultiTemperature.App.ViewEntities;
using Huaxin.MultiTemperature.App.ViewModels;
using NKnife.IoC;

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
#if !DEBUG
            this.GoFullscreen();
#endif
            ButtonClickEventManager();
            ControlEventManager();
            ViewModelChangedManager();
            PageContainer.Children.Add(DI.Get<HomePage>());
        }

        private void ButtonClickEventManager()
        {
            HomeButton.Click += (s, e) =>
            {
                PageContainer.Children.Clear();
                PageContainer.Children.Add(DI.Get<HomePage>());
            };
            ProjectAndDatasButton.Click += (s, e) =>
            {
                PageContainer.Children.Clear();
                PageContainer.Children.Add(DI.Get<ProjectAndDatasPage>());
            };
            MeterPointButton.Click += (s, e) =>
            {
                PageContainer.Children.Clear();
                PageContainer.Children.Add(DI.Get<MeterPointPage>());
            };
            MeterDatasButton.Click += (s, e) =>
            {
                PageContainer.Children.Clear();
                PageContainer.Children.Add(DI.Get<MeterDatasPage>());
            };
            PlotButton.Click += (s, e) =>
            {
                PageContainer.Children.Clear();
                PageContainer.Children.Add(DI.Get<PlotPage>());
            };
            OptionAndToolsButton.Click += (s, e) =>
            {
                PageContainer.Children.Clear();
                PageContainer.Children.Add(DI.Get<OptionAndToolsPage>());
            };
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
