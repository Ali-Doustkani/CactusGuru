using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService, EventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            GeneraCommand = new RelayCommand(_navigationService.GotoGenera);
            TaxaCommand = new RelayCommand(_navigationService.GotoTaxa);
            AddCollectionItemCommand = new RelayCommand(AddCollectionItem);
            SuppliersCommand = new RelayCommand(_navigationService.GotoSuppliers);
            CollectorsCommand = new RelayCommand(_navigationService.GotoCollectors);
            LabelPrintCommand = new RelayCommand(_navigationService.GotoLabelPrint);
            HomeCommand = new RelayCommand(_navigationService.GotoHome);
            ImageGalleryCommand = new RelayCommand(_navigationService.GotoImageList);
        }

        private readonly INavigationService _navigationService;

        public ICommand GeneraCommand { get; }
        public ICommand TaxaCommand { get; }
        public ICommand AddCollectionItemCommand { get; }
        public ICommand SuppliersCommand { get; }
        public ICommand CollectorsCommand { get; }
        public ICommand LabelPrintCommand { get; }
        public ICommand ImageGalleryCommand { get; }
        public ICommand HomeCommand { get; }

        private void AddCollectionItem()
        {
            _navigationService.GotoCollectionItemInserter();
        }
    }
}