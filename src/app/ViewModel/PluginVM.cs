using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

using ViewModel.Plugin;

namespace ViewModel
{
    public class PluginVM : ObservableObject
    {
        private readonly IPluginSetup _pluginSetup;

        public string PluginsPathFolder
        {
            get => _pluginSetup.PluginsPathFolder;
            set => _pluginSetup.PluginsPathFolder = value;
        }

        public IEnumerable<IDictionary<string, object>>? PluginsMetadata
        {
            get => _pluginSetup.PluginsMetadata;
        }

        public IEnumerable<IDictionary<string, object>>? SelectedPluginsMetadata
        {
            get => _pluginSetup.PluginsMetadata;
        }

        public ICommand UpdateCommand { get; private set; }

        public ICommand SetupCommand { get; private set; }

        public PluginVM(IPluginSetup pluginSetup)
        {
            _pluginSetup = pluginSetup;
            UpdateCommand = new RelayCommand(() =>
            {
                _pluginSetup.Update();
                OnPropertyChanged(nameof(PluginsMetadata));
                OnPropertyChanged(nameof(SelectedPluginsMetadata));
            });
            SetupCommand = new RelayCommand(() => _pluginSetup.Setup(SelectedPluginsMetadata));
        }
    }
}
