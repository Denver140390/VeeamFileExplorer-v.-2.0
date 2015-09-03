using System;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    public class NavigationEventArgs : EventArgs
    {
        public string Path;

        public NavigationEventArgs(string path)
        {
            Path = path;
        }
    }
}