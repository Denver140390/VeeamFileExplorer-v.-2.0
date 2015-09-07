using System;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public string Title { get; } = "Veeam FileViewModel Explorer v. 2.0";

        public AddressBarViewModel AddressBarViewModel { get; } = new AddressBarViewModel();
        public DirectoryTreeViewModel DirectoryTreeViewModel { get; } = new DirectoryTreeViewModel();
        public DirectoryContentViewModel DirectoryContentViewModel { get; } = new DirectoryContentViewModel();

        public MainViewModel()
        {
            DirectoryViewModel.SelectedDirectoryChanged += OnSelectedDirectoryChanged;
            AddressBarViewModel.NewPathApplied += OnNewPathApplied;
            DirectoryContentViewModel.Navigating += OnNavigating;
        }

        private void OnSelectedDirectoryChanged(object sender, EventArgs e)
        {
            var directoryViewModel = sender as DirectoryViewModel;
            if (directoryViewModel != null)
            {
                AddressBarViewModel.Path = directoryViewModel.FullPath;
                //DirectoryContentViewModel.Directory = directoryViewModel;
                DirectoryContentViewModel.LoadContentAsync(directoryViewModel.FullPath);
            }
        }

        private void OnNewPathApplied(object sender, EventArgs eventArgs)
        {
            //DirectoryContentViewModel.Directory = new DirectoryViewModel(AddressBarViewModel.Path);
            DirectoryContentViewModel.LoadContentAsync(AddressBarViewModel.Path);
        }

        private void OnNavigating(object sender, EventArgs eventArgs)
        {
            //AddressBarViewModel.Path = DirectoryContentViewModel.Directory.FullPath;
            AddressBarViewModel.Path = DirectoryContentViewModel.Path;
        }
    }
}
