using System.Drawing;

namespace Model
{
    public static class ColorManager
    {
        public static (byte, byte, byte, byte) ConvertHvsToArgb(double hue, double value,
            double saturation, byte alpha = 255)
        {
            var c = value * saturation;
            var x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
            var m = value - c;

            var r1 = 0d;
            var g1 = 0d;
            var b1 = 0d;
            switch (hue)
            {
                case >= 0 and < 60:
                    r1 = c;
                    g1 = x;
                    b1 = 0;
                    break;
                case >= 60 and < 120:
                    r1 = x;
                    g1 = c;
                    b1 = 0;
                    break;
                case >= 120 and < 180:
                    r1 = 0;
                    g1 = c;
                    b1 = x;
                    break;
                case >= 180 and < 240:
                    r1 = 0;
                    g1 = x;
                    b1 = c;
                    break;
                case >= 240 and < 300:
                    r1 = x;
                    g1 = 0;
                    b1 = c;
                    break;
                case >= 300 and <= 360:
                    r1 = c;
                    g1 = 0;
                    b1 = x;
                    break;
            }

            var r = byte.MaxValue * (r1 + m);
            var g = byte.MaxValue * (g1 + m);
            var b = byte.MaxValue * (b1 + m);

            return (alpha, (byte)r, (byte)g, (byte)b);
        }

        public static (double, double, double) ConvertRgbToHvs(byte red, byte green, byte blue,
            double hue = 0)
        {
            var r = red / (double)255;
            var g = green / (double)255;
            var b = blue / (double)255;
            var max = Math.Max(r, Math.Max(g, b));
            var min = Math.Min(r, Math.Min(g, b));
            var value = max;
            var saturation = max != 0 ? (max - min) / max : 0;
            if (max != min)
            {
                var h = 0d;
                if (max == r)
                {
                    h = (g - b) / (max - min);
                }
                else if (max == g)
                {
                    h = 2 + (b - r) / (max - min);
                }
                else
                {
                    h = 4 + (r - g) / (max - min);
                }
                h = h * 60;
                hue = h >= 0 ? h : 360 + h;
            }
            return (hue, value, saturation);
        }
    }
}
