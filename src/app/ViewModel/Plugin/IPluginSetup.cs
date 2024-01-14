using PluginContracts;

namespace ViewModel.Plugin
{
    public interface IPluginSetup
    {
        public string PluginsPathFolder { get; set; }

        public IEnumerable<IPlugin> Plugins { get; }

        public IEnumerable<IDictionary<string, object>>? PluginsMetadata { get; }

        public event EventHandler PluginsChanged;

        public void Update();

        public void Setup(IEnumerable<IDictionary<string, object>> pluginsMetadata);
    }
}
