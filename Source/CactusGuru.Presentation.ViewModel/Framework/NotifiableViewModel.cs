using CactusGuru.Infrastructure.EventAggregation;
using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public abstract class NotifiableViewModel : BaseViewModel
    {
        public NotifiableViewModel()
        {
            UnloadCommand = new RelayCommand(Unload);
        }

        private EventAggregator _eventAggregator;
        public EventAggregator EventAggregator
        {
            get { return _eventAggregator; }
            set
            {
                if (_eventAggregator == null && value != null)
                {
                    _eventAggregator = value;
                    _eventAggregator.Notify += _eventAggregator_Notify;
                }
                else
                    throw new InvalidOperationException("This property can be set only once with a not null value.");
            }
        }

        public ICommand UnloadCommand { get; private set; }

        protected void NotifyOthers(object value, OperationType operationType)
        {
            EventAggregator.NotifyOthers(value, operationType);
        }

        protected virtual void OnSomethingHappened(NotificationEventArgs info) { }

        private void Unload()
        {
            if (EventAggregator == null) return;
            EventAggregator.Notify -= _eventAggregator_Notify;
        }

        private void _eventAggregator_Notify(NotificationEventArgs e)
        {
            OnSomethingHappened(e);
        }
    }
}
