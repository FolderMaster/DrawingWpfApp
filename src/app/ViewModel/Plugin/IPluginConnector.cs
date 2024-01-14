using PluginContracts;

namespace ViewModel.Plugin
{
    public interface IPluginConnector
    {
        public IEnumerable<Lazy<IPlugin, IDictionary<string, object>>>? LazyPlugins { get; }

        public string PluginsPathFolder { get; set; }

        public void Update();
    }
}
