using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using VeeamFileExplorer_v._2._0.Helpers;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class DirectoryTreeViewModel : ViewModelBase
    {
        public List<DirectoryViewModel> LogicalDrives { get; } = new List<DirectoryViewModel>();

        public ObservableCollection<IDialogViewModel> Dialogs { get; } = new ObservableCollection<IDialogViewModel>();

        public DirectoryTreeViewModel()
        {
            foreach (string logicalDrive in Directory.GetLogicalDrives())
            {
                LogicalDrives.Add(new DirectoryViewModel(logicalDrive));
            }
        }

        public RelayCommand NewMessageBoxCommand => new RelayCommand(OnNewMessageBox);
        public void OnNewMessageBox()
        {
            new MessageBoxViewModel
            {
                Caption = "Message Box",
                Message = "This is a message box!",
                Image = MessageBoxImage.Information
            }.Show(this.Dialogs);
        }
    }
}
