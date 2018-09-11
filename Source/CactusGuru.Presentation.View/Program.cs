using CactusGuru.Presentation.View.Views;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels;

namespace CactusGuru.Presentation.View
{
    public static class Program
    {
        public static void Start(MainViewModel viewModel, INavigationService navService)
        {
            MyDXMessageBoxLocalizer.Active = new MyDXMessageBoxLocalizer();
            var main = new Views.Main { DataContext = viewModel };
            main.Show();
            navService.GotoHome();
        }
    }
}
