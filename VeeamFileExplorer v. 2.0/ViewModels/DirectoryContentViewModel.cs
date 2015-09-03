using System;
using System.Collections.Generic;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryContentViewModel : ViewModelBase
    {
        private DirectoryViewModel _directory;

        public DirectoryViewModel Directory
        {
            get { return _directory; }
            set
            {
                SetProperty(ref _directory, value, () => Directory);
                NavigationViewModel.AddNewHistoryItem(_directory.FullPath);
            }
        }
        public NavigationViewModel<string> NavigationViewModel { get; set; } = new NavigationViewModel<string>();

        public DirectoryContentViewModel()
        {
            NavigationViewModel.Navigating += OnNavigating;
        }

        private void OnNavigating(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
