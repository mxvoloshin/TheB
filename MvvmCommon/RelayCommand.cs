using System;
using System.Windows.Input;

namespace MvvmCommon
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;
        public RelayCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;

        private void OnCanExecuteChanged()
        {
            var evt = CanExecuteChanged;
            if (evt != null)
            {
                evt.Invoke(this, null);
            }
        }
    }
}