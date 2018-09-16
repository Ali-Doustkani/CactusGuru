using CactusGuru.Entry.CompositionRoot;
using CactusGuru.Infrastructure.Logging;
using CactusGuru.Presentation.View.Views;
using CactusGuru.Presentation.ViewModel.Services.Navigations;
using CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels;
using System;
using System.Windows;

namespace CactusGuru.Entry
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Implementation.ServiceLocationBase.Instance = ObjectFactory.Instance;
            ViewModelLocator.Resolver = new ViewModelFactory();
            var main = new Main { DataContext = ObjectFactory.Instance.GetInstance<MainViewModel>() };
            main.Show();
            ObjectFactory.Instance.GetInstance<INavigationService>().GotoHome();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var log = ObjectFactory.Instance.GetInstance<ILogger>();
            var ex = e.ExceptionObject as Exception;
            log.Fatal(ex.Message, ex);
        }
    }
}
