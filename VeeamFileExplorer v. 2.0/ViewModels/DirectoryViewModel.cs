using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryViewModel : ViewModelBase, IFileSystemEntityViewModel
    {
        private readonly DirectoryInfo _directoryInfo;
        private bool _isSelected;
        
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
                    OnSelectedDirectoryChanged(EventArgs.Empty);
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

        public List<IFileSystemEntityViewModel> Files
        {
            get
            {
                try
                {
                    return _directoryInfo.GetFiles().Select(entity => (IFileSystemEntityViewModel)new FileViewModel(entity.FullName)).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<IFileSystemEntityViewModel> Content
        {
            get
            {
                try
                {
                    var content = new List<IFileSystemEntityViewModel>();
                    content.AddRange(SubDirectories);
                    content.AddRange(Files);
                    return content;
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
        }

        public static event EventHandler SelectedDirectoryChanged;

        protected virtual void OnSelectedDirectoryChanged(EventArgs e)
        {
            SelectedDirectoryChanged?.Invoke(this, e);
        }
    }
}
