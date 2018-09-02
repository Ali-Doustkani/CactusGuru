using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action _methodToExecute;
        private readonly Action<object> _methodToExecuteWithParam;
        private readonly Func<bool> _canExecuteEvaluator;

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        { }

        public RelayCommand(Action<object> methodToExecute)
        {
            _methodToExecuteWithParam = methodToExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteEvaluator == null)
                return true;
            return _canExecuteEvaluator.Invoke();
        }

        public void Execute(object parameter)
        {
            if (_methodToExecute != null)
                _methodToExecute.Invoke();
            else
                _methodToExecuteWithParam.Invoke(parameter);
        }
    }
}
