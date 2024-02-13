using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace View.Converters
{
    public class AlphaToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            switch (int.Parse(parameter.ToString()))
            {
                case 0:
                    color.A = 0;
                    break;
                case 1:
                    color.A = 255;
                    break;
                default:
                    throw new ArgumentException();
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}
