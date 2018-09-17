using CactusGuru.Application.ViewProviders.Main;
using CactusGuru.Presentation.ViewModel.Framework;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels
{
    public class FirstPageViewModel : FormViewModel
    {
        public FirstPageViewModel(IFirstPageViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
            CollectionListCommand = new RelayCommand(() => Navigations.GotoCollectionItemList());
            ImageListCommand = new RelayCommand(() => Navigations.GotoImageList());
            CollectionItemCommand = new RelayCommand(() => Navigations.GotoCollectionItem());
            PrintCommand = new RelayCommand(() => Navigations.GotoLabelPrint());
        }

        private readonly IFirstPageViewProvider _viewProvider;

        public ICommand CollectionListCommand { get; }
        public ICommand ImageListCommand { get; }
        public ICommand CollectionItemCommand { get; }
        public ICommand PrintCommand { get; }
        public int ItemsCount { get; private set; }

        protected override async void OnLoad()
        {
            ItemsCount = await _viewProvider.GetItemsCount();
            OnPropertyChanged(nameof(ItemsCount));
            LoaderState.ToIdle();
        }
    }
}
