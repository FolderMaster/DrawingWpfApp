using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace View.Converters
{
    public class ByteToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (int.Parse(parameter.ToString())) switch
            {
                0 => Color.FromRgb((byte)value, 0, 0),
                1 => Color.FromRgb(0, (byte)value, 0),
                2 => Color.FromRgb(0, 0, (byte)value),
                _ => throw new ArgumentException()
            };
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}
