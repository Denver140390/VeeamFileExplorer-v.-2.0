using System.Collections.Generic;
using System.IO;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public string Title { get; } = "Veeam File Explorer v. 2.0";

        public List<DirectoryViewModel> LogicalDrives { get; } = new List<DirectoryViewModel>();

        public MainViewModel()
        {
            foreach (string logicalDrive in Directory.GetLogicalDrives())
            {
                LogicalDrives.Add(new DirectoryViewModel(logicalDrive));
            }
        }
    }
}
