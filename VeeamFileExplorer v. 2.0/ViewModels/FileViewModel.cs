using System;
using System.IO;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class FileViewModel : ViewModelBase, IFileSystemEntityViewModel
    {
        private readonly FileInfo _fileInfo;
        
        public string Name
        {
            get { return _fileInfo.Name; }
        }

        public DateTime LastModifiedTime
        {
            get { return _fileInfo.LastWriteTime; }
        }

        public string Extension
        {
            get { return _fileInfo.Extension; }
        }

        public long Size
        {
            get { return _fileInfo.Length; }
        }

        public string FullPath
        {
            get { return _fileInfo.FullName; }
        }

        public FileViewModel(string fullPath)
        {
            _fileInfo = new FileInfo(fullPath);
        }
    }
}
