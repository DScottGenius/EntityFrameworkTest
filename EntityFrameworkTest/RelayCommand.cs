using System;
using System.Windows.Input;

namespace EntityFrameworkTest
{
    class RelayCommand : ICommand
    {
        Action<object> ExecuteMethod;
        Func<object, bool> CanExecuteMethod;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            ExecuteMethod = executeMethod;
            CanExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteMethod != null)
            {
                return CanExecuteMethod(parameter);
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}
