using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
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

        private Task _task;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public string Path { get; private set; }

        public ObservableCollection<IFileSystemEntityViewModel> Content { get; } = new ObservableCollection<IFileSystemEntityViewModel>();

        public NavigationViewModel NavigationViewModel { get; set; } = new NavigationViewModel();

        public DirectoryContentViewModel()
        {
            NavigationViewModel.Navigating += OnNavigating;
        }
        
        public async void LoadContentAsync(string path)
        {
            if (_task != null && !_task.IsCompleted)
            {
                _cancellationTokenSource.Cancel();
                //_cancellationTokenSource = new CancellationTokenSource();
            }

            //_task = LoadContent(path);
            Content.Clear();
            Path = path;
            NavigationViewModel.Add(Path);

            //_cancellationTokenSource = new CancellationTokenSource();
            _task = Task.Run(() => LoadContent(path), _cancellationTokenSource.Token);
            await _task;
        }

        private void LoadContent(string path)
        {
            try
            {
                var directoryPaths = Directory.GetDirectories(path);
                foreach (var directoryPath in directoryPaths)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    var directory = new DirectoryViewModel(directoryPath);
                    Application.Current.Dispatcher.Invoke(() => Content.Add(directory));
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }

                var filePaths = Directory.GetFiles(path);
                foreach (var filePath in filePaths)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    var file = new FileViewModel(filePath);
                    Application.Current.Dispatcher.Invoke(() => Content.Add(file));
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
            }
            catch (Exception)
            {
                // ignored
            }
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
