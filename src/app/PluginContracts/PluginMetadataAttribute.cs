namespace PluginContracts
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PluginMetadataAttribute : Attribute, IPluginMetadata
    {
        public string Name { get; private set; }

        public string Version { get; private set; }

        public PluginMetadataAttribute(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}
