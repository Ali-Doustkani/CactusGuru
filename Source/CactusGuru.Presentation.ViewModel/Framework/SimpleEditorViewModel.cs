using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    /// <summary>
    /// For simple objects that are managed with a list in the same window.
    /// </summary>
    public class SimpleEditorViewModel<TRowItem> : EditorViewModel<TRowItem>
         where TRowItem : WorkingViewModel
    {
        public SimpleEditorViewModel(IDataEntryViewProvider dataProvider,
            IWorkingFactory<TRowItem> viewModelFactory,
            IDialogService dialogService,
            string title)
            : base(dataProvider, viewModelFactory, dialogService)
        {
            _dataProvider = dataProvider;
            _dialogService = dialogService;
            Title = title;
            LoadCommand = new RelayCommand(Load);
            SelectNextCommand = new RelayCommand(() => MoveTo(1));
            SelectPreviousCommand = new RelayCommand(() => MoveTo(-1));
        }

        private readonly IDataEntryViewProvider _dataProvider;
        private readonly IDialogService _dialogService;
        private string _filterText;
        private List<TRowItem> _originalSource;

        public ObservableCollection<TRowItem> ItemSource { get; private set; }
        public ICommand FocusOnSearchCommand { get; }
        public ICommand SelectNextCommand { get; }
        public ICommand SelectPreviousCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public override string Title { get; }

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    ItemSource = new ObservableCollection<TRowItem>(_originalSource);
                else
                    ItemSource = new ObservableCollection<TRowItem>(Search(value));
                OnPropertyChanged(nameof(ItemSource));
                SelectFirstItem();
                _filterText = value;
            }
        }

        private void Load()
        {
            PrepareForLoad();
            _originalSource = new List<TRowItem>();
            foreach (var item in _dataProvider.GetList())
                _originalSource.Add(_viewModelFactory.Create(item));
            ItemSource = new ObservableCollection<TRowItem>(_originalSource);
            OnPropertyChanged(nameof(ItemSource));
            SelectFirstItem();
        }

        private void SelectFirstItem()
        {
            if (!ItemSource.Any())
                WorkingItem = null;
            else
                WorkingItem = ItemSource.First();
        }

        protected override void AddImp()
        {
            try
            {
                var processedObject = _dataProvider.Add(WorkingItem.InnerObject);
                var newItem = _viewModelFactory.Create(processedObject);
                _originalSource.Add(newItem);
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

        protected override TRowItem DeleteImp()
        {
            try
            {
                var itemToDelete = WorkingItem;
                _dataProvider.Delete(itemToDelete.InnerObject);
                _originalSource.Remove(itemToDelete);
                ItemSource.Remove(itemToDelete);
                return itemToDelete;
            }
            catch (ErrorHappenedException ex)
            {
                _dialogService.Error(ex.Message);
                return null;
            }
        }

        private void MoveTo(int nextValue)
        {
            var currentIndex = ItemSource.ToList().IndexOf(WorkingItem);
            var newIndex = currentIndex + nextValue;
            if (newIndex < 0 || newIndex >= ItemSource.Count()) return;
            WorkingItem = ItemSource.ElementAt(newIndex);
            OnPropertyChanged(nameof(WorkingItem));
        }

        private IEnumerable<TRowItem> Search(string value)
        {
            return _originalSource.Where(x => x.FilterTarget.ToLower().Contains(value.ToLower()));
        }
    }
}