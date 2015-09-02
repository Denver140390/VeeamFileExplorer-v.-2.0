using System;
using System.Windows.Input;

namespace VeeamFileExplorer_v._2._0.Helpers
{
    class RelayCommand : ICommand
    {
        private Action _execute;

        public RelayCommand(Action execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("Action is null!");
            }

            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged;
    }
}
