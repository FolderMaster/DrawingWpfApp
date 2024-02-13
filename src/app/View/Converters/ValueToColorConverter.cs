using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using Model;

namespace View.Converters
{
    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hue = (double)value;
            var value1 = double.Parse(parameter.ToString());
            var color = ColorManager.ConvertHvsToArgb(hue, value1, 1);
            return Color.FromArgb(color.Item1, color.Item2, color.Item3, color.Item4);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}
