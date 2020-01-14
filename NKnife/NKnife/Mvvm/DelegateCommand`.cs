using System;
using System.Windows.Input;

namespace NKnife.Mvvm
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _CanExecuteAction;
        private readonly Action<T> _ExecuteAction;

        public DelegateCommand(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecuteAction)
        {
            _ExecuteAction = executeAction;
            _CanExecuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return _CanExecuteAction == null || _CanExecuteAction((T) parameter);
        }

        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _ExecuteAction((T) parameter);
            }
        }
    }
}