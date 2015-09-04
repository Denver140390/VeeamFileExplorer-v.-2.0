using System;

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
            NavigationViewModel.Navigating += OnNavigating;
        }

        private void OnNavigating(object sender, NavigationEventArgs args)
        {
            var directory = new DirectoryViewModel(args.Path);
            SetProperty(ref _directory, directory, () => Directory);
            Navigating?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Navigating;
    }
}
