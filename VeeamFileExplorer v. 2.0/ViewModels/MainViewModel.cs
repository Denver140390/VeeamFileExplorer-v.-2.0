using System;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public string Title { get; } = "Veeam FileViewModel Explorer v. 2.0";

        public DirectoryTreeViewModel DirectoryTreeViewModel { get; } = new DirectoryTreeViewModel();
        public CurrentDirectoryViewModel CurrentDirectoryViewModel { get; } = new CurrentDirectoryViewModel();

        public MainViewModel()
        {
            DirectoryViewModel.SelectedDirectoryChanged += SelectedDirectoryChanged;
        }

        private void SelectedDirectoryChanged(object sender, EventArgs e)
        {
            var directoryViewModel = sender as DirectoryViewModel;
            if (directoryViewModel != null)
            {
                CurrentDirectoryViewModel.SelectedDirectory = directoryViewModel;
            }
        }
    }
}
