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
            CollectionListCommand = new RelayCommand(GotoCollectionList);
            ImageListCommand = new RelayCommand(GotoImageList);
        }

        private readonly IFirstPageViewProvider _viewProvider;

        public ICommand CollectionListCommand { get; }
        public ICommand ImageListCommand { get; }

        public int ItemsCount { get; private set; }

        private void GotoCollectionList()
        {
            Navigations.GotoCollectionItemList();
        }

        private void GotoImageList()
        {
            Navigations.GotoImageList();
        }

        protected override void OnLoad()
        {
            ItemsCount = _viewProvider.GetItemsCount();
            OnPropertyChanged(nameof(ItemsCount));
        }
    }
}
