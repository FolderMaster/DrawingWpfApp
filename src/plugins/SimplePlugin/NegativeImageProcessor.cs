
using PluginContracts;

namespace SimplePlugin
{
    public class NegativeImageProcessor : IImageProcessor, IPlugin
    {
        private readonly int _colorByteCount = 4;

        public byte[] ProcessImage(byte[] image)
        {
            for (var i = 0; i < image.Length; i += _colorByteCount)
            {
                for(var n = 0; n < _colorByteCount - 1; ++n)
                {
                    image[i + n] = (byte)(byte.MaxValue - image[i + n]);
                }
            }
            return image;
        }
    }
}