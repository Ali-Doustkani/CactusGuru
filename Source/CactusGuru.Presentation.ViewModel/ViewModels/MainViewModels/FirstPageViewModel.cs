using CactusGuru.Application.ViewProviders.Main;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels
{
    public class FirstPageViewModel : BaseViewModel, INavigationViewModel
    {
        public FirstPageViewModel(IFirstPageViewProvider viewProvider, INavigationService navigationService)
        {
            _viewProvider = viewProvider;
            _navigationService = navigationService;
            CollectionListCommand = new RelayCommand(GotoCollectionList);
            ImageListCommand = new RelayCommand(GotoImageList);
        }

        private readonly IFirstPageViewProvider _viewProvider;
        private readonly INavigationService _navigationService;

        public ICommand CollectionListCommand { get; }
        public ICommand ImageListCommand { get; }

        public int ItemsCount { get; private set; }

        private void GotoCollectionList()
        {
            _navigationService.GotoCollectionItemList();
        }

        private void GotoImageList()
        {
            _navigationService.GotoImageList();
        }

        public void Load()
        {
           //ItemsCount = _viewProvider.GetItemsCount();
            OnPropertyChanged(nameof(ItemsCount));
        }
    }
}
