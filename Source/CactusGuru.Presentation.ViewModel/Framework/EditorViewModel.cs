﻿using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;
using System.Windows.Input;
using CactusGuru.Application.Common;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public abstract class EditorViewModel<TRowItem> : BaseViewModel
        where TRowItem : WorkingViewModel
    {
        protected EditorViewModel(IDataEntryViewProvider dataProvider, IWorkingFactory<TRowItem> viewModelFactory, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _dataProvider = dataProvider;
            _viewModelFactory = viewModelFactory;
            State = EditorViewModelState.View;
            LoadCommand = new RelayCommand(PrepareForLoad);
            PrepareForAddCommand = new RelayCommand(PrepareForAdd, () => State == EditorViewModelState.View);
            PrepareForEditCommand = new RelayCommand(PrepareForEdit, CanEditOrDelete);
            DeleteCommand = new RelayCommand(AskAndDelete, CanEditOrDelete);
            SaveCommand = new RelayCommand(Save, CanSave);
            SaveNewCommand = new RelayCommand(SaveNew, CanSaveNew);
            CancelCommand = new RelayCommand(Cancel, () => State != EditorViewModelState.View);
        }

        private TransferObjectBase _originalItem;
        private TRowItem _workingItem;

        private readonly IDataEntryViewProvider _dataProvider;
        private readonly IDialogService _dialogService;
        protected readonly IWorkingFactory<TRowItem> _viewModelFactory;
        public abstract string Title { get; }
        public bool IsEditorOn => State != EditorViewModelState.View;

        private EditorViewModelState _state;
        public EditorViewModelState State
        {
            get { return _state; }
            private set
            {
                _state = value;
                OnPropertyChanged(nameof(IsEditorOn));
                if(value== EditorViewModelState.View)
                {
                    DefaultControlFocused = true;
                    OnPropertyChanged(nameof(DefaultControlFocused));
                }
                else if (value == EditorViewModelState.Add || value == EditorViewModelState.Edit)
                {
                    FirstControlFocused = true;
                    OnPropertyChanged(nameof(FirstControlFocused));
                }
            }
        }
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
                if (value != null)
                    NotifyAllPropertiesChanged();
            }
        }

        public bool FirstControlFocused { get; set; }

        public bool DefaultControlFocused { get; set; }

        public abstract void NotifyAllPropertiesChanged();

        protected virtual void PrepareForLoad() { }

        public virtual void PrepareForAdd()
        {
            State = EditorViewModelState.Add;
            WorkingItem = _viewModelFactory.Create(_dataProvider.Build());
            OnPropertyChanged(nameof(WorkingItem));
        }

        public virtual void PrepareForEdit()
        {
            State = EditorViewModelState.Edit;
            _originalItem = _dataProvider.Copy(WorkingItem.InnerObject);
        }

        public bool ItemIsSelected()
        {
            return WorkingItem != null;
        }

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

        protected virtual void DeleteImp()
        {
            try
            {
                var itemToDelete = WorkingItem;
                _dataProvider.Delete(itemToDelete.InnerObject);
            }
            catch (ErrorHappenedException ex)
            {
                _dialogService.Error(ex.Message);
            }
        }

        private bool CanEditOrDelete()
        {
            return State == EditorViewModelState.View && ItemIsSelected();
        }

        private void SaveNew()
        {
            if (!CheckForSave()) return;
            if (State == EditorViewModelState.Add)
                PrepareForAdd();
        }

        protected virtual bool CanSaveNew()
        {
            return State == EditorViewModelState.Add;
        }

        private void Cancel()
        {
            if (!_dialogService.Ask("آیا از لغو عملیات اطمینان دارید؟")) return;
            if (State == EditorViewModelState.Edit)
                CancelEdit();
            State = EditorViewModelState.View;
            NotifyAllPropertiesChanged();
        }

        private void Save()
        {
            if (!CheckForSave()) return;
            State = EditorViewModelState.View;
        }

        private bool CheckForSave()
        {
            try
            {
                if (State == EditorViewModelState.Add)
                    AddImp();
                else if (State == EditorViewModelState.Edit)
                    EditImp();
                return true;
            }
            catch (OperationFailedException)
            {
                return false;
            }
        }

        protected virtual bool CanSave()
        {
            return State != EditorViewModelState.View;
        }

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

        protected string GetStringProperty(string name)
        {
            if (WorkingItem == null) return string.Empty;
            return (string)WorkingItem.GetType().GetProperty(name).GetValue(WorkingItem);
        }
    }
}