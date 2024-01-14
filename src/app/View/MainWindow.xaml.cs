using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ViewModel;
using ViewModel.Plugin;

namespace View
{
    public partial class MainWindow : Window
    {
        private IPluginSetup _pluginSetup = new DefaultPluginSetup(new MefPluginConnector(Path.GetFullPath("Plugins")));

        private MainVM _mainVM;

        public MainWindow()
        {
            InitializeComponent();
            _mainVM = new MainVM(_pluginSetup);
            _mainVM.Image = CreateImage();
            DataContext = _mainVM;

            var window = new PluginWindow(_pluginSetup);
            window.Show();
        }

        private BitmapFrame CreateImage()
        {
            MemoryStream stream = new MemoryStream();
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(
                    new VisualBrush(InkCanvas),
                    null,
                    new Rect(new Point(0, 0), new Point(InkCanvas.ActualWidth,
                    InkCanvas.ActualHeight)));
            }
            var rtb = new RenderTargetBitmap((int)600, (int)200,
                96, 96, PixelFormats.Default);

            rtb.Render(drawingVisual);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Save(stream);
            return BitmapFrame.Create(stream, BitmapCreateOptions.None,
                BitmapCacheOption.OnLoad);
        }

        private void InkCanvas_StrokeCollected(object sender,
            InkCanvasStrokeCollectedEventArgs e)
        {
            _mainVM.Image = CreateImage();
        }
    }
}