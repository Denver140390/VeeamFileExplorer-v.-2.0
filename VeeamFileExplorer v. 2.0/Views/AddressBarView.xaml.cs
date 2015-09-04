using System.Windows.Controls;
using System.Windows.Input;

namespace VeeamFileExplorer_v._2._0.Views
{
    public partial class AddressBarView : UserControl
    {
        public AddressBarView()
        {
            InitializeComponent();
        }

        private void PreviewTextBox_OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key != Key.Enter) return;

            var textBox = sender as TextBox;
            if (textBox == null) return;

            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpression?.UpdateSource();
        }
    }
}
