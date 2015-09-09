using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class MessageBoxViewModel : IDialogViewModel
    {
        public string Caption { get; set; }

        public string Message { get; set; }

        public MessageBoxButton Buttons { get; set; } = MessageBoxButton.OK;

        public MessageBoxImage Image { get; set; } = MessageBoxImage.None;

        public MessageBoxResult Result { get; set; }

        public MessageBoxViewModel(string message = "", string caption = "")
        {
            Message = message;
            Caption = caption;
        }

        public MessageBoxResult Show(IList<IDialogViewModel> collection)
        {
            collection.Add(this);
            return Result;
        }
    }
}
