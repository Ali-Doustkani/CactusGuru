using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    /// <summary>
    /// For simple objects that are managed with a list in the same window.
    /// </summary>
    public abstract class SimpleEditorViewModel<TRowItem> : EditorViewModel<TRowItem>
         where TRowItem : WorkingViewModel, new()
    {
        public SimpleEditorViewModel(IDataEntryViewProvider dataProvider)
            : base(dataProvider)
        {
            _dataProvider = dataProvider;
            SelectNextCommand = new RelayCommand(() => MoveTo(1));
            SelectPreviousCommand = new RelayCommand(() => MoveTo(-1));
            ClearFilterCommand = new RelayCommand(() => ItemSource.FilterText = string.Empty);
        }

        private readonly IDataEntryViewProvider _dataProvider;

        public ObservableBag<TRowItem> ItemSource { get; private set; }
        public ICommand FocusOnSearchCommand { get; }
        public ICommand SelectNextCommand { get; }
        public ICommand SelectPreviousCommand { get; }
        public ICommand ClearFilterCommand { get; }

        protected async override void OnLoad()
        {
            ItemSource = Bag.Of<TRowItem>()
                .WithConvertor<TransferObjectBase>(CreateWorkingObject)
                .WithSource(await _dataProvider.GetListAsync())
                .FilterBy(Filter)
                .Build();
            OnPropertyChanged(nameof(ItemSource));
            SelectFirstItem();
        }

        protected virtual bool Filter(TRowItem vm, string text)
        {
            return false;
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
                var newItem = CreateWorkingObject(processedObject);
                ItemSource.Add(newItem);
                WorkingItem = newItem;
                OnPropertyChanged(nameof(WorkingItem));
            }
            catch (ErrorHappenedException ex)
            {
                Dialog.Error(ex.Message);
                throw new OperationFailedException();
            }
        }

        protected override TRowItem DeleteImp()
        {
            try
            {
                var itemToDelete = WorkingItem;
                _dataProvider.Delete(itemToDelete.InnerObject);
                ItemSource.Remove(itemToDelete);
                NotifyOthers(itemToDelete.InnerObject, OperationType.Delete);
                return itemToDelete;
            }
            catch (ErrorHappenedException ex)
            {
                Dialog.Error(ex.Message);
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
    }
}