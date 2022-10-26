using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestTask.Commands
{
    internal class RelayCommand : ICommand
    {
        private Action<object> action;
        private Func<object, bool> predicate;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested+=value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            action = execute;
            predicate = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return predicate==null || predicate(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
