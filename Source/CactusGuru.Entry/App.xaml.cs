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
            CactusGuru.Presentation.View.Views.ViewModelLocator.Resolver = new ViewModelFactory();
            CactusGuru.Presentation.View.Main.Start(ObjectFactory.Instance.GetInstance<MainViewModel>(), ObjectFactory.Instance.GetInstance<INavigationService>());
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var log = ObjectFactory.Instance.GetInstance<ILogger>();
            var ex = e.ExceptionObject as Exception;
            log.Fatal(ex.Message, ex);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            CactusGuru.Presentation.View.Main.Exit();
        }
    }
}
