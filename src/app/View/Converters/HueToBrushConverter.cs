using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using Model;

namespace View.Converters
{
    public class HueToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = ColorManager.ConvertHvsToArgb((double)value, 1, 1);
            return new SolidColorBrush(Color.FromArgb(color.Item1, color.Item2, color.Item3,
                color.Item4));
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}
