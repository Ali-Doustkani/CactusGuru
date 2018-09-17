using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            SortCommand = new RelayCommand(Sort);
        }

        private readonly ICollectionItemListViewProvider _viewProvider;

        public ICommand GotoImageGallaryCommand { get; }
        public ICommand DeleteCurrentCollectionItemCommand { get; }
        public ICommand CopyNameCommand { get; }
        public ICommand EditCurrentCollectionItemCommand { get; }
        public ICommand SortCommand { get; }
        public CollectionItemViewModel SelectedCollectionItem { get; set; }
        public ObservableBag<CollectionItemViewModel> CollectionItems { get; private set; }
        public bool SortOnGenus { get; private set; }
        public bool SortOnCode { get; private set; }

        protected async override void OnLoad()
        {
            CollectionItems = await Bag.Of<CollectionItemViewModel>()
                .WithConvertor((Application.Common.CollectionItemDto dto) => new CollectionItemViewModel(_viewProvider.Convert(dto)))
                .WithId(x => x.InnerObject.Id)
                .FilterBy((item, text) => item.Name.Has(text) || item.Code == text || item.Info.Has(text))
                .Build();
            OnPropertyChanged(nameof(CollectionItems));
            AddListener(CollectionItems);
            SortOnGenus = true;
            await LoadData("Genus");
        }

        private async Task LoadData(string sortBy)
        {
            LoaderState.ToBusy();
            SortOnCode = sortBy == "Code";
            SortOnGenus = sortBy == "Genus";
            OnPropertyChanged(nameof(SortOnCode));
            OnPropertyChanged(nameof(SortOnGenus));
            var items = await _viewProvider.GetCollectionItemsAsync(sortBy);
            CollectionItems.Clear();
            foreach (var item in items)
                CollectionItems.Add(new CollectionItemViewModel(item));
            LoaderState.ToIdle();
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

        private async void Sort(object type)
        {
            await LoadData((string)type);
        }
    }
}