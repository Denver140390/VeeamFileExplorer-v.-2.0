using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryViewModel : ViewModelBase, IFileSystemEntityViewModel
    {
        private readonly DirectoryInfo _directoryInfo;
        private bool _isSelected;

        //TODO Setters
        public string Name
        {
            get { return _directoryInfo.Name; }
        }

        public DateTime LastModifiedTime
        {
            get { return _directoryInfo.LastWriteTime; }
        }

        public string Extension
        {
            get { return String.Empty; }
        }

        public long Size
        {
            get { return 0; }
        }

        public string FullPath
        {
            get { return _directoryInfo.FullName; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
        }

        public List<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _directoryInfo.GetDirectories().Select(entity => new DirectoryViewModel(entity.FullName)).ToList();
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
    }
}
