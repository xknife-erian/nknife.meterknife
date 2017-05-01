using System.Windows;

namespace Huaxin.MultiTemperature.App.Controls
{
    /// <summary>
    ///     StatisticsLabel.xaml 的交互逻辑
    /// </summary>
    public partial class StatisticsLabel
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(string), typeof(StatisticsLabel), new FrameworkPropertyMetadata(OnValueChanged));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(StatisticsLabel), new FrameworkPropertyMetadata(OnTitleChanged));

        public StatisticsLabel()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return (string) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StatisticsLabel label = (StatisticsLabel)d;
            label.ValueBlock.Text = e.NewValue.ToString();
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StatisticsLabel label = (StatisticsLabel)d;
            label.TitleBlock.Text = e.NewValue.ToString();
        }
    }
}