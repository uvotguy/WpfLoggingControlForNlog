using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System;

namespace LoggingControl
{
    public class LogItemBgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() switch
            {
                "Info" => Brushes.White,
                "Debug" => Brushes.Orange,
                "Trace" => Brushes.Black,
                "Warn" => Brushes.Yellow,
                "Error" => Brushes.Tomato,
                _ => Brushes.White,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
