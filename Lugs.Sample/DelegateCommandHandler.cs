using System;
using System.Windows.Input;

namespace Lugs.Sample
{
    public class DelegateCommandHandler : ICommand
    {
        private Action action;

        public DelegateCommandHandler(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }

        public event EventHandler CanExecuteChanged;
    }
}