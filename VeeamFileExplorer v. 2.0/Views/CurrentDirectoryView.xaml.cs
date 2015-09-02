using System.Windows.Controls;
using VeeamFileExplorer_v._2._0.ViewModels;

namespace VeeamFileExplorer_v._2._0.Views
{
    public partial class CurrentDirectoryView : UserControl
    {
        public CurrentDirectoryView()
        {
            InitializeComponent();
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGridRow = (DataGridRow)sender;
            var fileSystemEntity = dataGridRow.DataContext as IFileSystemEntityViewModel;
            fileSystemEntity?.Open();
        }
    }
}
