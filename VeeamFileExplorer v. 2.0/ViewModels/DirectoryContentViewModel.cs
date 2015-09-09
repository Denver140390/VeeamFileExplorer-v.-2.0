using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private const int CONTENT_PACK_LENGTH = 20; // amount of directories to load at once

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

            _task = Task.Run(() => LoadContent(path, _cancellationTokenSource), _cancellationTokenSource.Token);
            await _task;
        }

        private void LoadContent(string path, CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var directoryPaths = Directory.GetDirectories(path);
                var directories = new List<IFileSystemEntityViewModel>();
                foreach (var directoryPath in directoryPaths)
                {
                    directories.Add(new DirectoryViewModel(directoryPath));

                    if (directories.Count != CONTENT_PACK_LENGTH) continue;
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    Application.Current.Dispatcher.Invoke(() => LoadContentPart(directories));
                    directories.Clear();
                }
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
                Application.Current.Dispatcher.Invoke(() => LoadContentPart(directories));

                var filePaths = Directory.GetFiles(path);
                var files = new List<IFileSystemEntityViewModel>();
                foreach (var filePath in filePaths)
                {
                    files.Add(new FileViewModel(filePath));

                    if (files.Count != CONTENT_PACK_LENGTH) continue;
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    Application.Current.Dispatcher.Invoke(() => LoadContentPart(files));
                    files.Clear();
                }
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
                Application.Current.Dispatcher.Invoke(() => LoadContentPart(files));
            }
            catch (Exception)
            {
                return;
            }
        }

        private void LoadContentPart(List<IFileSystemEntityViewModel> contentParts)
        {
            foreach (var entity in contentParts)
            {
                if (entity.FullPath.Contains(Path))
                {
                    Content.Add(entity);
                }
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
