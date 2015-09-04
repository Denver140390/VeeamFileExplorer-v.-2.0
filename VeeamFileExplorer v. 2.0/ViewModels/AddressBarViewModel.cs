using System;
using System.IO;
using VeeamFileExplorer_v._2._0.Helpers;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class AddressBarViewModel : ViewModelBase
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            set
            {
                SetProperty(ref _path, value, () => Path);
            }
        }

        public RelayCommand ApplyNewPathCommand { get; private set; }

        public AddressBarViewModel()
        {
            ApplyNewPathCommand = new RelayCommand(ApplyNewPath);
        }

        private void ApplyNewPath()
        {
            if (Directory.Exists(_path))
                OnNewPathApplied();
        }

        public event EventHandler NewPathApplied;

        private void OnNewPathApplied()
        {
            NewPathApplied?.Invoke(this, EventArgs.Empty);
        }
    }
}
