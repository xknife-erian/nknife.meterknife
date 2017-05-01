using System.Windows;
using System.Windows.Controls;

namespace Huaxin.MultiTemperature.App.Controls
{
    /// <summary>
    ///     MeterPointPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MeterPointPanel : UserControl
    {
        public static readonly DependencyProperty PointProperty = DependencyProperty.Register(
            nameof(Point), typeof(ushort), typeof(MeterPointPanel), new PropertyMetadata(OnPointChanged));

        public static readonly DependencyProperty ComputeValueProperty = DependencyProperty.Register(
            nameof(ComputeValue), typeof(double), typeof(MeterPointPanel), new PropertyMetadata(OnComputeValueChanged));

        public static readonly DependencyProperty MeterValueProperty = DependencyProperty.Register(
            nameof(MeterValue), typeof(double), typeof(MeterPointPanel), new PropertyMetadata(OnMeterValueChanged));

        public MeterPointPanel()
        {
            InitializeComponent();
        }

        public ushort Point
        {
            get { return (ushort) GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }

        public double ComputeValue
        {
            get { return (double) GetValue(ComputeValueProperty); }
            set{SetValue(ComputeValueProperty,value);}
        }

        public double MeterValue
        {
            get { return (double) GetValue(MeterValueProperty); }
            set { SetValue(MeterValueProperty, value); }
        }

        private static void OnPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = (MeterPointPanel) d;
            panel.PointLabel.Text = e.NewValue.ToString();
        }

        private static void OnComputeValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = (MeterPointPanel) d;
            panel.ComputeValueBlock.Text = e.NewValue.ToString();
        }

        private static void OnMeterValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = (MeterPointPanel) d;
            panel.MeterValueBlock.Text = e.NewValue.ToString();
        }
    }
}