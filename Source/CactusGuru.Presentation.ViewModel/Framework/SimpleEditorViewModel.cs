using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Presentation.ViewModel.Framework.DataSourceManagement;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    /// <summary>
    /// For simple objects that are managed with a list in the same window.
    /// </summary>
    public abstract class SimpleEditorViewModel<TRowItem> : EditorViewModel<TRowItem>
         where TRowItem : WorkingViewModel
    {
        protected SimpleEditorViewModel(IDataEntryViewProvider dataProvider,
            IWorkingFactory<TRowItem> viewModelFactory,
            IDialogService dialogService)
            : base(dataProvider, viewModelFactory, dialogService)
        {
            _dataProvider = dataProvider;
            _dialogService = dialogService;
            LoadCommand = new RelayCommand(Load);
          //  FocusOnSearchCommand = new RelayCommand(windowController.FocusOnSearch);
        }

        private readonly IDataEntryViewProvider _dataProvider;
        private readonly IDialogService _dialogService;

        private IDataSource<TRowItem> _itemSource;
        public IDataSource<TRowItem> ItemSource
        {
            get { return _itemSource; }
            set
            {
                ArgumentChecker.CheckNull(value);
                _itemSource = value;
                ClearFilterTextCommand = new RelayCommand(ItemSource.ClearTextFilter);
            }
        }

        public ICommand ClearFilterTextCommand { get; private set; }
        public ICommand FocusOnSearchCommand { get; private set; }

        private void Load()
        {
            PrepareForLoad();
            var items = new List<TRowItem>();
            foreach (var item in _dataProvider.GetList())
                items.Add(_viewModelFactory.Create(item));
            ItemSource.Load(items);
            SelectFirstItem();
        }

        protected void SelectFirstItem()
        {
            if (!ItemSource.Any()) return;
            WorkingItem = ItemSource.First();
            OnPropertyChanged(nameof(WorkingItem));
        }

        protected override void AddImp()
        {
            try
            {
                var processedObject = _dataProvider.Add(WorkingItem.InnerObject);
                var newItem = _viewModelFactory.Create(processedObject);
                ItemSource.Add(newItem);
                WorkingItem = newItem;
                OnPropertyChanged(nameof(WorkingItem));
            }
            catch (ErrorHappenedException ex)
            {
                _dialogService.Error(ex.Message);
                throw new OperationFailedException();
            }
        }

        protected override void DeleteImp()
        {
            try
            {
                var itemToDelete = WorkingItem;
                _dataProvider.Delete(itemToDelete.InnerObject);
                ItemSource.Remove(itemToDelete);
            }
            catch (ErrorHappenedException ex)
            {
                _dialogService.Error(ex.Message);
            }
        }
    }
}
