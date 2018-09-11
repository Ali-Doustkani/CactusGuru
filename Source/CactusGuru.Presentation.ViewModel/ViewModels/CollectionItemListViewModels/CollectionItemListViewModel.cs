using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using System;
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
            GotoImageGallaryCommand = new RelayCommand(() => Navigations.GotoCollectionItemImageGallary(SelectedCollectionItem.InnerObject.Id), CanGotoImageGallery);
            EditCurrentCollectionItemCommand = new RelayCommand(() => Navigations.GotoCollectionItemUpdater(SelectedCollectionItem.InnerObject.Id));
            DeleteCurrentCollectionItemCommand = new RelayCommand(DeleteCurrentCollectionItem);
            CopyNameCommand = new RelayCommand(CopyNameToClipboard);
            State = new LoaderState();
        }

        private readonly ICollectionItemListViewProvider _viewProvider;

        public ICommand GotoImageGallaryCommand { get; }
        public ICommand DeleteCurrentCollectionItemCommand { get; }
        public ICommand CopyNameCommand { get; }
        public ICommand EditCurrentCollectionItemCommand { get; }
        public CollectionItemViewModel SelectedCollectionItem { get; set; }
        public ObservableBag<CollectionItemViewModel> CollectionItems { get; private set; }
        public LoaderState State { get; }

        protected async override void OnLoad()
        {
            CollectionItems = Bag.Of<CollectionItemViewModel>()
                .WithConvertor((Application.Common.CollectionItemDto dto) => new CollectionItemViewModel(_viewProvider.Convert(dto)))
                .WithId(x => x.InnerObject.Id)
                .FilterBy((item, text) => item.Name.Has(text) || item.Code == text || item.Info.Has(text))
                .Build();
            OnPropertyChanged(nameof(CollectionItems));
            AddListener(CollectionItems);

            var items = await _viewProvider.GetCollectionItemsAsync();
            foreach (var item in items)
                CollectionItems.Add(new CollectionItemViewModel(item));
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

        private CollectionItemViewModel GetCollectionItem(Guid id)
        {
            return CollectionItems.Single(x => x.InnerObject.Id == id);
        }
    }
}