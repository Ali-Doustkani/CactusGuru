using CactusGuru.Entry.CompositionRoot;
using CactusGuru.Infrastructure.Logging;
using CactusGuru.Presentation.ViewModel.NavigationService;
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
            CactusGuru.Presentation.View.Views.ViewModelLocator.Resolver = new ViewModelFactory();
            CactusGuru.Presentation.View.Program.Start(ObjectFactory.Instance.GetInstance<MainViewModel>(), ObjectFactory.Instance.GetInstance<INavigationService>());
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var log = ObjectFactory.Instance.GetInstance<ILogger>();
            var ex = e.ExceptionObject as Exception;
            log.Fatal(ex.Message, ex);
        }
    }
}
