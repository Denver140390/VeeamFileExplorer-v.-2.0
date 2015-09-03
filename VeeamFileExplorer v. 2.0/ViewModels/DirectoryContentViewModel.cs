using System;
using System.Collections.Generic;
using System.Windows.Navigation;

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
                NavigationViewModel.Add(_directory.FullPath);
            }
        }
        public NavigationViewModel NavigationViewModel { get; set; } = new NavigationViewModel();

        public DirectoryContentViewModel()
        {
            NavigationViewModel.Navigating += NavigationViewModelOnNavigating;
        }

        private void NavigationViewModelOnNavigating(object sender, NavigationEventArgs args)
        {
            SetProperty(ref _directory, new DirectoryViewModel(args.Path), () => Directory);
        }
    }
}
