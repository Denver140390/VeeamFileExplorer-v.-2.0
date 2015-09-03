using System.Collections.Generic;
using VeeamFileExplorer_v._2._0.Helpers;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class NavigationViewModel : ViewModelBase
    {
        private int _index;

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

        private List<string> History { get; } = new List<string>();

        public NavigationViewModel()
        {
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public void Add(string item)
        {
            if (History.Count > 0)
                History.RemoveRange(_index, History.Count - _index - 1);
            History.Add(item);
            _index = History.Count - 1;
            CanGoBack = History.Count > 1;
        }

        private void GoBack()
        {
            CanGoForward = true;
            _index--;
            CanGoBack = _index > 0;

            if (_index < 0) return;
            string path = History[_index];
            OnNavigating(new NavigationEventArgs(path));
        }

        private void GoForward()
        {
            CanGoBack = true;
            _index++;
            CanGoForward = _index < History.Count - 1;

            if (_index > History.Count - 1) return;
            string path = History[_index];
            OnNavigating(new NavigationEventArgs(path));
        }
        
        public event NavigationEventHandler Navigating;
        public delegate void NavigationEventHandler(object sender, NavigationEventArgs args);

        private void OnNavigating(NavigationEventArgs args)
        {
            Navigating?.Invoke(this, args);
        }
    }
}
