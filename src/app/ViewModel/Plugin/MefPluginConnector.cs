using PluginContracts;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace ViewModel.Plugin
{
    public class MefPluginConnector : IPluginConnector
    {
        private readonly CompositionContainer _compositionContainer;

        private AggregateCatalog _aggregateCatalog = new();

        private DirectoryCatalog? _directoryCatalog = null;

        private string _pluginPathFolder;

        [ImportMany(typeof(IPlugin))]
        public IEnumerable<Lazy<IPlugin, IDictionary<string, object>>>? LazyPlugins
            { get; private set; } = null;

        public string PluginsPathFolder
        {
            get => _pluginPathFolder;
            set
            {
                if (_pluginPathFolder != value)
                {
                    if(Directory.Exists(value))
                    {
                        _pluginPathFolder = value;
                        if (_directoryCatalog != null)
                        {
                            _aggregateCatalog.Catalogs.Remove(_directoryCatalog);
                        }
                        _directoryCatalog = new DirectoryCatalog(PluginsPathFolder);
                        _aggregateCatalog.Catalogs.Add(_directoryCatalog);
                    }
                    else
                    {
                        throw new ArgumentException(nameof(PluginsPathFolder),
                            nameof(value));
                    }
                }
            }
        }

        public MefPluginConnector()
        {
            _compositionContainer = new CompositionContainer(_aggregateCatalog);
            _aggregateCatalog.Catalogs.Add(new AssemblyCatalog
                (typeof(MefPluginConnector).Assembly));
        }

        public MefPluginConnector(string pluginsPathFolder) : this()
        {
            PluginsPathFolder = pluginsPathFolder;
        }

        public void Update()
        {
            LazyPlugins = _compositionContainer.GetExports<IPlugin, IDictionary<string, object>>();
        }
    }
}
