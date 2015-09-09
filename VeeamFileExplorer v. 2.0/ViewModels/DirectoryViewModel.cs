using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using VeeamFileExplorer_v._2._0.Helpers;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryViewModel : ViewModelBase, IFileSystemEntityViewModel
    {
        private readonly DirectoryInfo _directoryInfo;
        private bool _isSelected;

        public RelayCommand RequestOpenInWindowsExplorerCommand { get; private set; }

        public string Name => _directoryInfo.Name;

        public DateTime LastModifiedTime => _directoryInfo.LastWriteTime;

        public string Extension => "Folder";

        public long Size => 0;

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

        public List<IFileSystemEntityViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _directoryInfo.GetDirectories().Select(entity => (IFileSystemEntityViewModel)new DirectoryViewModel(entity.FullName)).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public DirectoryViewModel(string parentName)
        {
            _directoryInfo = new DirectoryInfo(parentName);
            RequestOpenInWindowsExplorerCommand = new RelayCommand(OpenInWindowsExplorer);
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
