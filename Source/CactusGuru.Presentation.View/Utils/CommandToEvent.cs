using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Utils
{
    public class CommandToEvent : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler Executed;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Executed?.Invoke(this, EventArgs.Empty);
        }
    }
}
