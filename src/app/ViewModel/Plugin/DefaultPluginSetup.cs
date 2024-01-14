using PluginContracts;

namespace ViewModel.Plugin
{
    public class DefaultPluginSetup : IPluginSetup
    {
        private readonly IPluginConnector _pluginConnector;

        private IEnumerable<Lazy<IPlugin, IDictionary<string, object>>>? _lazyPlugins;

        private IEnumerable<IDictionary<string, object>>? _pluginsMetadata;

        private IEnumerable<IPlugin>? _plugins;

        public event EventHandler PluginsChanged;

        private IEnumerable<Lazy<IPlugin, IDictionary<string, object>>>? LazyPlugins
        {
            get => _lazyPlugins;
            set
            {
                if(_lazyPlugins != value)
                {
                    if(value != null)
                    {
                        _pluginsMetadata = value.Select(l => l.Metadata);
                    }
                    _lazyPlugins = value;
                }
            }
        }

        public string PluginsPathFolder
        {
            get => _pluginConnector.PluginsPathFolder;
            set => _pluginConnector.PluginsPathFolder = value;
        }

        public IEnumerable<IDictionary<string, object>>? PluginsMetadata => _pluginsMetadata;

        public IEnumerable<IPlugin>? Plugins => _plugins;

        public DefaultPluginSetup(IPluginConnector pluginConnector)
        {
            _pluginConnector = pluginConnector;
        }

        public void Update()
        {
            _pluginConnector.Update();
            LazyPlugins = _pluginConnector.LazyPlugins;
        }

        public void Setup(IEnumerable<IDictionary<string, object>> pluginsMetadata)
        {
            if (LazyPlugins == null)
            {
                throw new InvalidOperationException();
            }
            _plugins = LazyPlugins.Where(l => pluginsMetadata.Contains(l.Metadata)).Select(l => l.Value);
            PluginsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
