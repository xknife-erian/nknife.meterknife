using System;
using System.Globalization;
using System.Windows.Data;

namespace Huaxin.MultiTemperature.App.Commons
{
    /// <summary>
    /// 为bool值取反的值转换器
    /// </summary>
    public class NegateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool ? !(bool) value : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}