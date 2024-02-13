using Model;
using System.Net.WebSockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup.Localizer;
using System.Windows.Media;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickerControl.xaml
    /// </summary>
    public partial class ColorPickerControl : UserControl
    {
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        
        public byte Alpha
        {
            get => (byte)GetValue(AlphaProperty);
            set => SetValue(AlphaProperty, value);
        }

        public byte Red
        {
            get => (byte)GetValue(RedProperty);
            set => SetValue(RedProperty, value);
        }

        public byte Blue
        {
            get => (byte)GetValue(BlueProperty);
            set => SetValue(BlueProperty, value);
        }

        public byte Green
        {
            get => (byte)GetValue(GreenProperty);
            set => SetValue(GreenProperty, value);
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Saturation
        {
            get => (double)GetValue(SaturationProperty);
            set => SetValue(SaturationProperty, value);
        }

        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register
            (nameof(Color), typeof(Color), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(OnColorChanged));

        public static readonly DependencyProperty AlphaProperty = DependencyProperty.Register
            (nameof(Alpha), typeof(byte), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(OnArgbChanged));

        public static readonly DependencyProperty RedProperty = DependencyProperty.Register
            (nameof(Red), typeof(byte), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(OnArgbChanged));

        public static readonly DependencyProperty BlueProperty = DependencyProperty.Register
            (nameof(Blue), typeof(byte), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(OnArgbChanged));

        public static readonly DependencyProperty GreenProperty = DependencyProperty.Register
            (nameof(Green), typeof(byte), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(OnArgbChanged));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register
            (nameof(Value), typeof(double), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(OnVgbChanged));

        public static readonly DependencyProperty SaturationProperty = DependencyProperty.Register
            (nameof(Saturation), typeof(double), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(OnVgbChanged));

        public static readonly DependencyProperty HueProperty = DependencyProperty.Register
            (nameof(Hue), typeof(double), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(OnVgbChanged));

        public ColorPickerControl()
        {
            InitializeComponent();
        }

        private static void OnColorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPickerControl)sender;
            var color = (Color)e.NewValue;

            colorPicker.Alpha = color.A;
            colorPicker.Red = color.R;
            colorPicker.Blue = color.B;
            colorPicker.Green = color.G;
            var hvs = ColorManager.ConvertRgbToHvs(color.R, color.G, color.B, colorPicker.Hue);
            colorPicker.Hue = hvs.Item1;
            colorPicker.Value = hvs.Item2;
            colorPicker.Saturation = hvs.Item3;
        }

        private static void OnArgbChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPickerControl)sender;
            var color = colorPicker.Color;
            var value = (byte)e.NewValue;
            if(e.Property == AlphaProperty)
            {
                color.A = value;
            }
            else if (e.Property == RedProperty)
            {
                color.R = value;
            }
            else if (e.Property == GreenProperty)
            {
                color.G = value;
            }
            else if (e.Property == BlueProperty)
            {
                color.B = value;
            }
            colorPicker.Color = color;
        }

        private static void OnVgbChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPickerControl)sender;
            var value = (double)e.NewValue;
            if (e.Property == ValueProperty)
            {
                colorPicker.Value = value;
            }
            else if (e.Property == SaturationProperty)
            {
                colorPicker.Saturation = value;
            }
            else if (e.Property == HueProperty)
            {
                colorPicker.Hue = value;
            }
            var color = ColorManager.ConvertHvsToArgb(colorPicker.Hue, colorPicker.Value,
                colorPicker.Saturation, colorPicker.Alpha);
            colorPicker.Color = Color.FromArgb(color.Item1, color.Item2, color.Item3, color.Item4);
        }
    }
}
