using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;
using System.Windows.Ink;
using System.Windows.Media.Imaging;
using System.Windows;

using ViewModel.Plugin;

using PluginContracts;

namespace ViewModel
{
    public partial class MainVM : ObservableObject
    {
        private readonly IPluginSetup _pluginSetup;

        private IImageProcessor? _imageProcessor;

        [ObservableProperty]
        private DrawingAttributes drawingAttributes = new DrawingAttributes();

        [ObservableProperty]
        private Brush drawingBrush;

        [ObservableProperty]
        private string pluginMetadata;

        [ObservableProperty]
        private BitmapSource? image;

        public Color DrawingColor
        {
            get => DrawingAttributes.Color;
            set
            {
                var color = DrawingAttributes.Color;
                if (SetProperty(ref color, value))
                {
                    DrawingAttributes.Color = color;
                    DrawingBrush = new SolidColorBrush(color);
                }
            }
        }

        public RelayCommand Command { get; private set; }

        public MainVM(IPluginSetup pluginSetup)
        {
            _pluginSetup = pluginSetup;
            _pluginSetup.PluginsChanged += PluginSetup_PluginsChanged;
            DrawingBrush = new SolidColorBrush(DrawingColor);
            Command = new RelayCommand(UpdateImage, () => _imageProcessor != null);
        }

        public void UpdateImage()
        {
            int width = Image.PixelWidth;
            int height = Image.PixelHeight;
            int bytesPerPixel = (Image.Format.BitsPerPixel + 7) / 8;
            byte[] pixelData = new byte[width * height * bytesPerPixel];
            Image.CopyPixels(pixelData, width * bytesPerPixel, 0);
            WriteableBitmap writeableBitmap = new WriteableBitmap
                (width, height, 96, 96, PixelFormats.Pbgra32, null);
            writeableBitmap.WritePixels(new Int32Rect
                (0, 0, width, height), 
                _imageProcessor.ProcessImage(pixelData), 
                width * bytesPerPixel, 0);
            Image = writeableBitmap;
        }

        private void PluginSetup_PluginsChanged(object? sender, EventArgs e)
        {
            var plugins = _pluginSetup.Plugins;
            if(plugins.Count() > 0)
            {
                _imageProcessor = plugins.First() as IImageProcessor;
                Command?.NotifyCanExecuteChanged();
            }
        }
    }
}
