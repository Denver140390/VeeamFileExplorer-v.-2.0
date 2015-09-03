using System.Windows.Controls;
using System.Windows.Input;
using VeeamFileExplorer_v._2._0.ViewModels;

namespace VeeamFileExplorer_v._2._0.Views
{
    public partial class DirectoryContentView : UserControl
    {
        public DirectoryContentView()
        {
            InitializeComponent();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGridRow = (DataGridRow)sender;
            var fileSystemEntity = dataGridRow.DataContext as IFileSystemEntityViewModel;
            fileSystemEntity?.Open();
        }
    }
}
