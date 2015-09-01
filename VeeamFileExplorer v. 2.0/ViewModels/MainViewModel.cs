namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public string Title { get; } = "Veeam File Explorer v. 2.0";

        public DirectoryTreeViewModel DirectoryTreeViewModel { get; } = new DirectoryTreeViewModel();

        public MainViewModel()
        {
            DirectoryViewModel.SelectedDirectoryChanged += SelectedDirectoryChanged;
        }

        private void SelectedDirectoryChanged(object sender, System.EventArgs e)
        {
            var directoryViewModel = sender as DirectoryViewModel;
            if (directoryViewModel != null)
            {
                var path = directoryViewModel.FullPath;
            }
        }
    }
}
