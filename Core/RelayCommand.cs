using System;
using System.Windows.Input;

namespace Core
{
    public class RelayCommand : ICommand
    {
        Func<object, bool> canExecute;
        Action<object> execute;
        public RelayCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }
        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }

            else return false;
        }

        public void Execute(object parameter)
        {
            if (execute != null)
            {
                execute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}