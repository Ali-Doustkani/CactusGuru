using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemListViewModels
{
    public class CollectionItemListViewModel : FormViewModel
    {
        public CollectionItemListViewModel(ICollectionItemListViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
            _loaderWorker = new BackgroundWorker();
            _loaderWorker.WorkerReportsProgress = true;
            _loaderWorker.DoWork += _loaderWorker_DoWork;
            _loaderWorker.RunWorkerCompleted += _loaderWorker_RunWorkerCompleted;
            _loaderWorker.ProgressChanged += _loaderWorker_ProgressChanged;
            GotoImageGallaryCommand = new RelayCommand(() => Navigations.GotoCollectionItemImageGallary(SelectedCollectionItem.InnerObject.Id), CanGotoImageGallery);
            EditCurrentCollectionItemCommand = new RelayCommand(() => Navigations.GotoCollectionItemUpdater(SelectedCollectionItem.InnerObject.Id));
            DeleteCurrentCollectionItemCommand = new RelayCommand(DeleteCurrentCollectionItem);
            CopyNameCommand = new RelayCommand(CopyNameToClipboard);
            CollectionItems = new ObservableCollection<CollectionItemViewModel>();
            State = new LoaderState();
        }

        private readonly ICollectionItemListViewProvider _viewProvider;
        private readonly BackgroundWorker _loaderWorker;

        public ICommand GotoImageGallaryCommand { get; private set; }
        public ICommand DeleteCurrentCollectionItemCommand { get; private set; }
        public ICommand CopyNameCommand { get; private set; }
        public ICommand EditCurrentCollectionItemCommand { get; private set; }
        public CollectionItemViewModel SelectedCollectionItem { get; set; }
        public ObservableCollection<CollectionItemViewModel> CollectionItems { get; }
        public LoaderState State { get; }

        protected override void OnLoad()
        {
            _loaderWorker.RunWorkerAsync();
        }

        private void _loaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_viewProvider.GetCollectionItemsAsync(LoadItems))
            { }
        }

        private void LoadItems(CollectionItemAsyncDto dto)
        {
            var viewModels = new List<CollectionItemViewModel>();
            foreach (var item in dto.LoadedItems)
                viewModels.Add(new CollectionItemViewModel(item));
            _loaderWorker.ReportProgress(0, viewModels);
        }

        private void _loaderWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ((List<CollectionItemViewModel>)e.UserState).ForEach(CollectionItems.Add);
        }

        private void _loaderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            State.ToIdle();
        }

        private void DeleteCurrentCollectionItem()
        {
            if (!Dialog.Ask("آیا از حذف گیاه خود اطمینان دارید؟"))
                return;
            var itemToDelete = SelectedCollectionItem;
            SelectedCollectionItem = CollectionItems.Last();
            OnPropertyChanged(nameof(SelectedCollectionItem));
            _viewProvider.DeleteCollectionItem(itemToDelete.InnerObject.Id);
            CollectionItems.Remove(itemToDelete);
        }

        private void CopyNameToClipboard()
        {
            Clipboard.SetText(SelectedCollectionItem.Name);
        }

        private bool CanGotoImageGallery()
        {
            return SelectedCollectionItem != null;
        }

        protected override void OnSomethingHappened(NotificationEventArgs info)
        {
            var collectionItem = info.Object as CollectionItemDto;
            if (collectionItem == null)
                return;

            if (info.OperationType == OperationType.Add)
                ItemAdded(collectionItem.Id);
            else if (info.OperationType == OperationType.Update)
                ItemEdited(collectionItem.Id);
            else if (info.OperationType == OperationType.Delete)
                ItemDeleted(collectionItem.Id);
        }

        private void ItemAdded(Guid id)
        {
            CollectionItems.Add(new CollectionItemViewModel(_viewProvider.GetCollectionItem(id)));
        }

        private void ItemEdited(Guid id)
        {
            var itemToEdit = GetCollectionItem(id);
            itemToEdit.InnerObject = _viewProvider.GetCollectionItem(id);
        }

        private void ItemDeleted(Guid id)
        {
            var itemToDelete = GetCollectionItem(id);
            CollectionItems.Remove(itemToDelete);
        }

        private CollectionItemViewModel GetCollectionItem(Guid id)
        {
            return CollectionItems.Single(x => x.InnerObject.Id == id);
        }
    }
}
