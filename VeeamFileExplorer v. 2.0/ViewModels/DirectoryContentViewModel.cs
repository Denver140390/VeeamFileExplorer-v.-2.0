namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryContentViewModel : ViewModelBase
    {
        private DirectoryViewModel _directory;

        public DirectoryViewModel Directory
        {
            get { return _directory; }
            set { SetProperty(ref _directory, value, () => Directory); }
        }
    }
}
