using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryContentViewModel : ViewModelBase
    {
//        private DirectoryViewModel _directory;

//        public DirectoryViewModel Directory
//        {
//            get { return _directory; }
//            set
//            {
//                SetProperty(ref _directory, value, () => Directory);
//                NavigationViewModel.Add(_directory.FullPath);
//            }
//        }

        public string Path { get; private set; }

        public ObservableCollection<IFileSystemEntityViewModel> Content { get; } = new ObservableCollection<IFileSystemEntityViewModel>();

        public NavigationViewModel NavigationViewModel { get; set; } = new NavigationViewModel();

        public DirectoryContentViewModel()
        {
            NavigationViewModel.Navigating += OnNavigating;
        }

        public async void LoadContentAsync(string path)
        {
            Content.Clear();
            Path = path;
            NavigationViewModel.Add(Path);

            await Task.Run(() =>
            {
                var directoryPaths = Directory.GetDirectories(path);
                foreach (var directoryPath in directoryPaths)
                {
                    var directory = new DirectoryViewModel(directoryPath);
                    Application.Current.Dispatcher.Invoke(() => Content.Add(directory));
                }

                var filePaths = Directory.GetFiles(path);
                foreach (var filePath in filePaths)
                {
                    var file = new FileViewModel(filePath);
                    Application.Current.Dispatcher.Invoke(() => Content.Add(file));
                }
            });
        }

        private void OnNavigating(object sender, NavigationEventArgs args)
        {
            //var directory = new DirectoryViewModel(args.Path);
            //SetProperty(ref _directory, directory, () => Directory);
            LoadContentAsync(args.Path);
            Navigating?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Navigating;
    }
}
