using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService)
        {
            MenuItemCommand = new RelayCommand(destination =>
            {
                var cmd = destination.ToString().ToLower();
                if (cmd == "genus")
                    navigationService.GotoGenera();
                else if (cmd == "taxon")
                    navigationService.GotoTaxa();
                else if (cmd == "collectionitem")
                    navigationService.GotoCollectionItemInserter();
                else if (cmd == "supplier")
                    navigationService.GotoSuppliers();
                else if (cmd == "collector")
                    navigationService.GotoCollectors();
                else if (cmd == "printlabel")
                    navigationService.GotoLabelPrint();
                else if (cmd == "gallary")
                    navigationService.GotoImageList();
            });
        }

        public ICommand MenuItemCommand { get; }
    }
}