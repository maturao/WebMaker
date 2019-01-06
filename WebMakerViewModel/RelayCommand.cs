using System;
using System.Windows.Input;

namespace WebMaker.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> action;

        public RelayCommand(Action<object> action)
        {
            this.action = action;
        }

        public RelayCommand(Action action)
        {
            this.action = (parameter) => action();
        }

        public event EventHandler CanExecuteChanged = (sender, args) => { };

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action(parameter);
    }
}