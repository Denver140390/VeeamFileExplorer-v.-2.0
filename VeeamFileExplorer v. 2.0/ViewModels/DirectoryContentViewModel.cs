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
                _cancellationTokenSource = new CancellationTokenSource();
            }

            Path = path;
            NavigationViewModel.Add(Path);
            Content.Clear();

            _task = Task.Run(() => LoadContent(path, _cancellationTokenSource.Token), _cancellationTokenSource.Token);
            await _task;
        }

        private void LoadContent(string path, CancellationToken cancellationToken)
        {
            try
            {
                var directoryPaths = Directory.GetDirectories(path);
                foreach (var directoryPath in directoryPaths)
                {
                    var directory = new DirectoryViewModel(directoryPath);
                    cancellationToken.ThrowIfCancellationRequested();
                    Application.Current.Dispatcher.Invoke(() => Content.Add(directory));
                }

                var filePaths = Directory.GetFiles(path);
                foreach (var filePath in filePaths)
                {
                    var file = new FileViewModel(filePath);
                    cancellationToken.ThrowIfCancellationRequested();
                    Application.Current.Dispatcher.Invoke(() => Content.Add(file));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void OnNavigating(object sender, NavigationEventArgs args)
        {
            LoadContentAsync(args.Path);
            Navigating?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Navigating;
    }
}
