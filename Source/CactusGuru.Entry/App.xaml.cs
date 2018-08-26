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
        private Resolver _resolver;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            _resolver = new Resolver();
            CactusGuru.Presentation.View.Main.Start(_resolver.GetInstance<MainViewModel>(), _resolver.GetInstance<INavigationService>());
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var log = _resolver.GetInstance<ILogger>();
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
