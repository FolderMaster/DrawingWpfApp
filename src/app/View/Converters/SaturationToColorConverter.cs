using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using Model;

namespace View.Converters
{
    public class SaturationToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            CultureInfo culture)
        {
            var hue = (double)values[0];
            var value = (double)values[1];
            var saturation = double.Parse(parameter.ToString());
            var color = ColorManager.ConvertHvsToArgb(hue, value, saturation);
            return Color.FromArgb(color.Item1, color.Item2, color.Item3, color.Item4);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}
