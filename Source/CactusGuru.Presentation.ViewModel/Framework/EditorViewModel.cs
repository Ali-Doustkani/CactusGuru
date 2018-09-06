using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public abstract class EditorViewModel<TRowItem> : NotifiableViewModel, INotifyDataErrorInfo
        where TRowItem : WorkingViewModel
    {
        protected EditorViewModel(IDataEntryViewProvider dataProvider, IWorkingFactory<TRowItem> viewModelFactory, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _dataProvider = dataProvider;
            _viewModelFactory = viewModelFactory;
            _changeableCollections = new List<IChangeableCollection>();
            State = new EditorState();
            Rules = new Rules(RaiseErrorsChanged);
            LoadCommand = new RelayCommand(PrepareForLoad);
            PrepareForAddCommand = new RelayCommand(PrepareForAdd, () => State.IsView);
            PrepareForEditCommand = new RelayCommand(PrepareForEdit, CanEditOrDelete);
            DeleteCommand = new RelayCommand(AskAndDelete, CanEditOrDelete);
            SaveCommand = new RelayCommand(Save, CanSave);
            SaveNewCommand = new RelayCommand(SaveNew, CanSaveNew);
            CancelCommand = new RelayCommand(Cancel, () => State.IsNotView);
        }

        private TransferObjectBase _originalItem;
        private TRowItem _workingItem;
        private readonly List<IChangeableCollection> _changeableCollections;
        private readonly IDataEntryViewProvider _dataProvider;
        private readonly IDialogService _dialogService;
        protected readonly IWorkingFactory<TRowItem> _viewModelFactory;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public abstract string Title { get; }
        public EditorState State { get; }
        public Rules Rules { get; }
        public ICommand LoadCommand { get; protected set; }
        public ICommand PrepareForAddCommand { get; private set; }
        public ICommand PrepareForEditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SaveNewCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public TRowItem WorkingItem
        {
            get { return _workingItem; }
            set
            {
                _workingItem = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public bool HasErrors => Rules.AnyError();

        protected virtual void PrepareForLoad() { }

        public virtual void PrepareForAdd()
        {
            State.ToAdd();
            WorkingItem = _viewModelFactory.Create(_dataProvider.Build());
            OnPropertyChanged(nameof(WorkingItem));
        }

        public virtual void PrepareForEdit()
        {
            State.ToEdit();
            _originalItem = _dataProvider.Copy(WorkingItem.InnerObject);
        }

        public bool ItemIsSelected()
        {
            return WorkingItem != null;
        }

        public IEnumerable GetErrors(string propertyName) => Rules.GetErrors(propertyName);

        protected virtual void AddImp()
        {
            try
            {
                _dataProvider.Add(WorkingItem.InnerObject);
            }
            catch (ErrorHappenedException ex)
            {
                _dialogService.Error(ex.Message);
                throw new OperationFailedException();
            }
        }

        protected virtual void EditImp()
        {
            try
            {
                WorkingItem.InnerObject = _dataProvider.Update(WorkingItem.InnerObject);
            }
            catch (ErrorHappenedException ex)
            {
                _dialogService.Error(ex.Message);
                throw new OperationFailedException();
            }
        }

        protected virtual TRowItem DeleteImp()
        {
            try
            {
                var itemToDelete = WorkingItem;
                _dataProvider.Delete(itemToDelete.InnerObject);
                return itemToDelete;
            }
            catch (ErrorHappenedException ex)
            {
                _dialogService.Error(ex.Message);
                return null;
            }
        }

        protected void AddListener(IChangeableCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            _changeableCollections.Add(collection);
        }

        protected override sealed void OnSomethingHappened(NotificationEventArgs info)
        {
            foreach (var col in _changeableCollections)
                col.Change(info);
        }

        private bool CanEditOrDelete()
        {
            return State.IsView && ItemIsSelected();
        }

        private void SaveNew()
        {
            if (!CheckForSave()) return;
            if (State.IsAdd)
                PrepareForAdd();
        }

        private void Cancel()
        {
            if (!_dialogService.Ask("آیا از لغو عملیات اطمینان دارید؟")) return;
            if (State.IsEdit)
                CancelEdit();
            State.ToView();
            Rules.ClearErrors();
            OnPropertyChanged(string.Empty);
        }

        private void Save()
        {
            if (!CheckForSave()) return;
            State.ToView();
        }

        private bool CheckForSave()
        {
            try
            {
                if (State.IsAdd)
                    AddImp();
                else if (State.IsEdit)
                    EditImp();
                return true;
            }
            catch (OperationFailedException)
            {
                return false;
            }
        }

        protected virtual bool CanSaveNew() => State.IsAdd && !HasErrors;

        protected virtual bool CanSave() => State.IsNotView && !HasErrors;

        private void AskAndDelete()
        {
            if (!_dialogService.Ask("آیا از حذف تامین کننده ی انتخابی اطمینان دارید؟"))
                return;
            DeleteImp();
        }

        private void CancelEdit()
        {
            _dataProvider.CopyTo(_originalItem, WorkingItem.InnerObject);
        }

        private void RaiseErrorsChanged(string propname)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propname));
        }
    }
}