using VeeamFileExplorer_v._2._0.Helpers;

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

        public RelayCommand OpenCommand { get; private set; }

        public SelectedDirectoryViewModel()
        {
            OpenCommand = new RelayCommand(Open);
        }

        public void Open()
        {
            
        }
    }
}
