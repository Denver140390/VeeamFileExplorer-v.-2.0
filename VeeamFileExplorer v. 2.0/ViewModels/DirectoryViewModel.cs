using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryViewModel : ViewModelBase, IFileSystemEntityViewModel
    {
        private readonly DirectoryInfo _directoryInfo;

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

        public DirectoryInfo Parent { get; private set; }

        public List<DirectoryViewModel> Content
        {
            get
            {
                try
                {
                    return _directoryInfo.GetDirectories().Select(entity => new DirectoryViewModel(entity)).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public DirectoryViewModel(DirectoryInfo directoryInfo)
        {
            //TODO ??? DirectoryInfo as parameter...
            _directoryInfo = directoryInfo;
            Parent = directoryInfo;
        }
    }
}
