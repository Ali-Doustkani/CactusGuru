using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public abstract class FormViewModel : BaseViewModel
    {
        public FormViewModel()
        {
            LoadCommand = new RelayCommand(OnLoad);
            UnloadCommand = new RelayCommand(OnUnload);
        }

        private EventAggregator _eventAggregator;
        private IDialogService _dialog;
        private INavigationService _navigations;

        public ICommand LoadCommand { get; private set; }
        public ICommand UnloadCommand { get; private set; }

        public EventAggregator EventAggregator
        {
            get { return _eventAggregator; }
            set
            {
                SetOnceField(ref _eventAggregator, value);
                _eventAggregator.Notify += _eventAggregator_Notify;
            }
        }

        public IDialogService Dialog
        {
            get { return _dialog; }
            set { SetOnceField(ref _dialog, value); }
        }

        public INavigationService Navigations
        {
            get { return _navigations; }
            set { SetOnceField(ref _navigations, value); }
        }

        protected void NotifyOthers(object value, OperationType operationType)
        {
            EventAggregator.NotifyOthers(value, operationType);
        }

        protected virtual void OnSomethingHappened(NotificationEventArgs info) { }

        protected virtual void OnLoad() { }

        private void OnUnload()
        {
            if (EventAggregator == null) return;
            EventAggregator.Notify -= _eventAggregator_Notify;
        }

        private void _eventAggregator_Notify(NotificationEventArgs e)
        {
            OnSomethingHappened(e);
        }

        private void SetOnceField<T>(ref T field, T value)
        {
            if (field == null && value != null)
                field = value;
            else
                throw new InvalidOperationException($"The property of type {typeof(T).Name} could be set only once with a not null value.");
        }
    }
}