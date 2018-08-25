using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels;

namespace CactusGuru.Presentation.View
{
    public static class Main
    {
        public static void Start(MainViewModel viewModel, INavigationService navService)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
            var main = new Views.Main { DataContext = viewModel };
            main.Show();
            navService.GotoHome();
        }

        public static void Exit()
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.SaveApplicationThemeName();
        }
    }
}
