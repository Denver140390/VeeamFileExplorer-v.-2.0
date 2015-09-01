using System.Collections.Generic;
using System.IO;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryTreeViewModel
    {
        public List<DirectoryViewModel> LogicalDrives { get; } = new List<DirectoryViewModel>();

        public DirectoryTreeViewModel()
        {
            foreach (string logicalDrive in Directory.GetLogicalDrives())
            {
                LogicalDrives.Add(new DirectoryViewModel(logicalDrive));
            }
        }
    }
}
