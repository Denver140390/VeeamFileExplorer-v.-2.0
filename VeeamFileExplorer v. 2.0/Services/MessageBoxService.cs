using System.Windows;

namespace VeeamFileExplorer_v._2._0.Services
{
    class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
