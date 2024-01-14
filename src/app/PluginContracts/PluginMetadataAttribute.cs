using System.ComponentModel.Composition;

namespace PluginContracts
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PluginMetadataAttribute : ExportAttribute, IPluginMetadata
    {
        public string Name { get; private set; }

        public string Version { get; private set; }

        public PluginMetadataAttribute(string name, string version) : base(typeof(IImageProcessor))
        {
            Name = name;
            Version = version;
        }
    }
}
