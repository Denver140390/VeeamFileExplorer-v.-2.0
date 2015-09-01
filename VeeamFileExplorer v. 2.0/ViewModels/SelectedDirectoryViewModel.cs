namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class SelectedDirectoryViewModel : ViewModelBase
    {
        private DirectoryViewModel _selectedDirectory;

        public DirectoryViewModel SelectedDirectory
        {
            get { return _selectedDirectory; }
            set
            {
                SetProperty(ref _selectedDirectory, value, () => SelectedDirectory);
            }
        }
    }
}
