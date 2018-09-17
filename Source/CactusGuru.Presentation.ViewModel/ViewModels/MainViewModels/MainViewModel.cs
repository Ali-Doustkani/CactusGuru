using CactusGuru.Presentation.ViewModel.Framework;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels
{
    public class MainViewModel : FormViewModel
    {
        public MainViewModel()
        {
            MenuItemCommand = new RelayCommand(destination =>
            {
                var cmd = destination.ToString().ToLower();
                if (cmd == "genus")
                    Navigations.GotoGenera();
                else if (cmd == "taxon")
                    Navigations.GotoTaxa();
                else if (cmd == "collectionitem")
                    Navigations.GotoCollectionItem();
                else if (cmd == "supplier")
                    Navigations.GotoSuppliers();
                else if (cmd == "collector")
                    Navigations.GotoCollectors();
                else if (cmd == "printlabel")
                    Navigations.GotoLabelPrint();
                else if (cmd == "gallary")
                    Navigations.GotoImageList();
            });
            HomeCommand = new RelayCommand(() => Navigations.GotoHome());
        }

        public ICommand MenuItemCommand { get; private set; }
        public ICommand HomeCommand { get; private set; }
    }
}