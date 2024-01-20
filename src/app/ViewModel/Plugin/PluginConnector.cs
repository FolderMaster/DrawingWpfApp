using PluginContracts;
using System.IO;
using System.Reflection;

namespace ViewModel.Plugin
{
    public class PluginConnector : IPluginConnector
    {
        private string? _pluginsPathFolder;

        private IEnumerable<Lazy<IPlugin, IDictionary<string, object>>>? _lazyPlugins;

        public IEnumerable<Lazy<IPlugin, IDictionary<string, object>>>? LazyPlugins
        {
            get => _lazyPlugins;
            private set => _lazyPlugins = value;
        }

        public string? PluginsPathFolder
        {
            get => _pluginsPathFolder;
            set => _pluginsPathFolder = value;
        }

        private Lazy<IPlugin, IDictionary<string, object>> CreateLazy(string path)
        {
            var asseblyName = AssemblyName.GetAssemblyName(path);
            var metadata = new Dictionary<string, object>()
            {
                ["Name"] = asseblyName.Name,
                ["Version"] = asseblyName.Version
            };
            var pluginFactory = () =>
            {
                var assembly = Assembly.LoadFrom(path);
                var plugin = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IPlugin)) && t.IsClass && !t.IsAbstract).First();
                return Activator.CreateInstance(plugin) as IPlugin;
            };
            return new Lazy<IPlugin, IDictionary<string, object>>(pluginFactory, metadata);
        }

        public void Update()
        {
            if (PluginsPathFolder != null)
            {
                var files = Directory.GetFiles(PluginsPathFolder, "*.dll");
                var result = new List<Lazy<IPlugin, IDictionary<string, object>>>();
                foreach (var file in files)
                {
                    result.Add(CreateLazy(file));
                }
                LazyPlugins = result;
            }
        }

        public PluginConnector() { }

        public PluginConnector(string pluginsPathFolder) : this() => PluginsPathFolder = pluginsPathFolder;
    }
}
