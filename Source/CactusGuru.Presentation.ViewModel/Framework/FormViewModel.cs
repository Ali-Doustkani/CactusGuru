using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using CactusGuru.Presentation.ViewModel.Services.Navigations;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public abstract class FormViewModel : BaseViewModel
    {
        public FormViewModel()
        {
            LoadCommand = new RelayCommand(OnLoad);
            UnloadCommand = new RelayCommand(OnUnload);
            _items = new List<IChangeableCollection>();
            LoaderState = new LoaderState();
        }

        private EventAggregator _eventAggregator;
        private IDialogService _dialog;
        private INavigationService _navigations;
        private readonly List<IChangeableCollection> _items;

        public ICommand LoadCommand { get; private set; }
        public ICommand UnloadCommand { get; private set; }
        public LoaderState LoaderState { get; }

        public EventAggregator EventAggregator
        {
            get { return _eventAggregator; }
            set
            {
                SetOnceField(ref _eventAggregator, value);
                _eventAggregator.Notify += EventAggregatorNotified;
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

        protected virtual void OnSomethingHappened(NotificationEventArgs info)
        {
            foreach (var col in _items)
                col.Change(info);
        }

        protected virtual void OnLoad() { }

        protected void AddListener(IChangeableCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            _items.Add(collection);
        }

        private void OnUnload()
        {
            if (EventAggregator == null) return;
            EventAggregator.Notify -= EventAggregatorNotified;
        }

        private void EventAggregatorNotified(NotificationEventArgs e)
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