using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VeeamFileExplorer_v._2._0.Helpers;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryViewModel : ViewModelBase, IFileSystemEntityViewModel
    {
        private readonly DirectoryInfo _directoryInfo;
        private bool _isSelected;
        private bool _isExpanded;

        private static readonly DirectoryViewModel _dummy = new DirectoryViewModel();

        private const int DIRECTORIES_PACK_LENGTH = 20; // amount of directories to load at once

        public RelayCommand RequestOpenInWindowsExplorerCommand { get; private set; }

        public string Name => _directoryInfo.Name;

        public DateTime LastModifiedTime => _directoryInfo.LastWriteTime;

        public string Extension => "Folder";

        public long Size => -1;

        public string FullPath => _directoryInfo.FullName;

        public BitmapSource Icon => new BitmapImage(new Uri("pack://application:,,,/Images/folder.png"));

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value, () => IsSelected);
                if (_isSelected)
                    OnSelectedDirectoryChanged();
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                LoadSubDirectoriesAsync();
            }
        }

        private async void LoadSubDirectoriesAsync()
        {
            if (!_isExpanded || !CanAccessDirectory()) return;

            SubDirectories.Clear();
            var directoryInfos = _directoryInfo.GetDirectories();
            var directories = new List<DirectoryViewModel>();
            foreach (var directoryInfo in directoryInfos)
            {
                await Task.Run(() => directories.Add(new DirectoryViewModel(directoryInfo.FullName)));

                if (directories.Count != DIRECTORIES_PACK_LENGTH) continue;
                foreach (var directory in directories)
                {
                    SubDirectories.Add(directory);
                }
                directories.Clear();
            }
            foreach (var directory in directories)
            {
                SubDirectories.Add(directory);
            }
        }

        public ObservableCollection<IFileSystemEntityViewModel> SubDirectories { get; } = new ObservableCollection<IFileSystemEntityViewModel>();

        private DirectoryViewModel()
        {
            // Dummy directory
            _directoryInfo = new DirectoryInfo("C:\\"); //TODO Need something more logical as a dummy
        }

        public DirectoryViewModel(string parentName)
        {
            _directoryInfo = new DirectoryInfo(parentName);
            if (CanAccessDirectory())
            {
                SubDirectories.Add(_dummy);
            }
            RequestOpenInWindowsExplorerCommand = new RelayCommand(OpenInWindowsExplorer);
        }

        private bool CanAccessDirectory()
        {
            try
            {
                var directoryInfos = _directoryInfo.GetDirectories();
                return directoryInfos.Length != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void OpenInWindowsExplorer()
        {
            Process.Start(FullPath);
        }

        public void Open()
        {
            OnSelectedDirectoryChanged();
        }

        public static event EventHandler SelectedDirectoryChanged;

        private void OnSelectedDirectoryChanged()
        {
            SelectedDirectoryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
