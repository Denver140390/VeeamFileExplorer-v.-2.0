using System;
using System.Collections.Generic;
using VeeamFileExplorer_v._2._0.Helpers;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class NavigationViewModel<T> : ViewModelBase
    {
        private bool _canGoBack;
        private bool _canGoForward;
        public RelayCommand GoBackCommand { get; private set; }
        public RelayCommand GoForwardCommand { get; private set; }

        public bool CanGoBack
        {
            get { return _canGoBack; }
            set { SetProperty(ref _canGoBack, value, () => CanGoBack); }
        }

        public bool CanGoForward
        {
            get { return _canGoForward; }
            set { SetProperty(ref _canGoForward, value, () => CanGoForward); }
        }

        private List<T> History { get; } = new List<T>();

        public NavigationViewModel()
        {
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public void AddNewHistoryItem(T path)
        {
            History.Add(path);
        }

        private void GoBack()
        {
            CanGoForward = true;
            OnNavigating();
        }

        private void GoForward()
        {
            CanGoBack = true;
            OnNavigating();
        }

        public event EventHandler Navigating;

        private void OnNavigating()
        {
            Navigating?.Invoke(this, EventArgs.Empty);
        }
    }
}
